using AutoMapper;
using GEWAR;
using Google;
using Jiwar.DTOs.DesignDto;
using Jiwar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Jiwar.Hubs;
using Jiwar.Enum;
using GEWAR.Models; // For Notification model if likely in GEWAR namespace based on previous checks

namespace Jiwar.Services.DesignerProposalService
{
    public class DesignerProposalService : IDesignerProposalService
    {
        private readonly GiwarContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationHub> _hubContext;

        public DesignerProposalService(GiwarContext context, IMapper mapper, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public async Task<ProposalDto> SendProposalAsync(string designerId, ProposalDto dto)
        {
            var request = await _context.DesignRequests.FindAsync(dto.RequestID);
            if (request == null)
                throw new Exception("Design request not found");

            if (request.Status == "InProgress" || request.Status == "Completed")
                throw new Exception("This request is no longer accepting proposals");

            var alreadySubmitted = await _context.DesignerProposals
                .AnyAsync(p => p.DesignRequestID == dto.RequestID && p.DesignerID == designerId);

            if (alreadySubmitted)
                throw new Exception("You already submitted a proposal for this request");

            var proposal = _mapper.Map<DesignerProposal>(dto);

           
            proposal.DesignRequestID = dto.RequestID;
            proposal.DesignerID = designerId;

            proposal.Designer = null;
            proposal.DesignRequest = null;

            proposal.StatusEnumReq = GEWAR.Models.StatusEnumReqPro.Pending;

            _context.DesignerProposals.Add(proposal);

            request.Status = "HasProposals";


            await _context.SaveChangesAsync();

            // Notify the owner
            if (!string.IsNullOrEmpty(request.UserID))
            {
                // 1. Save to Database
                var notification = new Notification
                {
                    UserID = request.UserID,
                    Title = "New Proposal",
                    Message = $"You have received a new proposal for your design request {request.Id}.",
                    NotificationType = NotificationType.Request,
                    SentDate = DateTime.Now,
                    IsRead = false
                };

                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();

                // 2. Send Real-time Notification WITH SOUND trigger
                await _hubContext.Clients.User(request.UserID).SendAsync("ReceiveNotification", new 
                {
                    title = notification.Title,
                    message = notification.Message,
                    type = notification.NotificationType.ToString(),
                    sentDate = notification.SentDate,
                    playSound = true
                });
            }

            return _mapper.Map<ProposalDto>(proposal);
        }

        public async Task<List<ProposalForOwnerDto>> GetProposalsForRequestAsync(int requestId)
        {
            var proposals = await _context.DesignerProposals
                .Include(p => p.Designer)
                .Where(p => p.DesignRequestID == requestId)
                .Select(p => new ProposalForOwnerDto
                {
                    Id = p.Id,
                    EstimatedCost = p.EstimatedCost,
                    EstimatedDays = p.EstimatedDays,
                    ProposalDescription = p.ProposalDescription,
                    DesignerName = p.Designer.User.Name,
                    DesignerEmail = p.Designer.User.Email,
                    Status = p.StatusEnumReq
                })
                .ToListAsync();

            return proposals;
        }

        public async Task<IEnumerable<ProposalDto>> GetProposalsForDesignerAsync(string designerId)
        {
            var proposals = await _context.DesignerProposals
                .Where(p => p.DesignerID == designerId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProposalDto>>(proposals);
        }

        public async Task<List<ProposalForOwnerDto>> ChooseProposalAsync(int proposalId, string ownerId)
        {
            var selected = await _context.DesignerProposals
                .Include(p => p.DesignRequest)
                .Include(p => p.Designer)
                .ThenInclude(d => d.User)
                .FirstOrDefaultAsync(p => p.Id == proposalId);

            if (selected == null)
                throw new Exception("Proposal not found");

            if (selected.DesignRequest.UserID != ownerId)
                throw new Exception("You are not the owner of this request");

            var allProposals = await _context.DesignerProposals
                .Where(p => p.DesignRequestID == selected.DesignRequestID)
                .ToListAsync();

            selected.StatusEnumReq = GEWAR.Models.StatusEnumReqPro.Accepted;

            foreach (var p in allProposals)
            {
                if (p.Id != proposalId)
                    p.StatusEnumReq = GEWAR.Models.StatusEnumReqPro.Rejected;
            }

            selected.DesignRequest.Status = "InProgress";

            await _context.SaveChangesAsync();

            // Notify the designer that their proposal was accepted
            if (selected.Designer != null && !string.IsNullOrEmpty(selected.DesignerID))
            {
                var notification = new Notification
                {
                    UserID = selected.Designer.InteriorDesignerID, 
                    Title = "Proposal Accepted!",
                    Message = $"Your proposal for request #{selected.DesignRequestID} ({selected.DesignRequest.PreferredStyle}) has been accepted by the owner.",
                    NotificationType = NotificationType.Offer,
                    SentDate = DateTime.Now,
                    IsRead = false,
                    RelatedId = selected.DesignRequestID.ToString()
                };

                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();

                // Send Real-time Notification WITH SOUND trigger for frontend
                await _hubContext.Clients.User(selected.Designer.InteriorDesignerID).SendAsync("ReceiveNotification", new 
                {
                    title = notification.Title,
                    message = notification.Message,
                    type = notification.NotificationType.ToString(),
                    relatedId = notification.RelatedId,
                    sentDate = notification.SentDate,
                    playSound = true // Hint for frontend to play the 'tin tin' sound
                });
            }

            return await GetProposalsForRequestAsync(selected.DesignRequestID);
        }

        public async Task<bool> DeliverProposalAsync(int proposalId, string designerId, string notes)
        {
            var proposal = await _context.DesignerProposals
                .Include(p => p.DesignRequest)
                .FirstOrDefaultAsync(p => p.Id == proposalId && p.DesignerID == designerId);

            if (proposal == null)
                throw new Exception("Proposal not found or you are not the designer of this proposal.");

            if (proposal.StatusEnumReq != GEWAR.Models.StatusEnumReqPro.Accepted)
                throw new Exception("Only accepted proposals can be delivered.");

            proposal.StatusEnumReq = GEWAR.Models.StatusEnumReqPro.Delivered;
            proposal.DeliveredAt = DateTime.UtcNow;
            proposal.DeliveryNotes = notes;
            proposal.DesignRequest.Status = "Completed";

            await _context.SaveChangesAsync();

            // Notify the Property Owner
            if (!string.IsNullOrEmpty(proposal.DesignRequest.UserID))
            {
                var notification = new Notification
                {
                    UserID = proposal.DesignRequest.UserID,
                    Title = "Project Delivered! 🎉",
                    Message = $"Designer has completed your project (Request #{proposal.DesignRequestID}). Please review the final designs.",
                    NotificationType = NotificationType.Request,
                    SentDate = DateTime.Now,
                    IsRead = false,
                    RelatedId = proposalId.ToString()
                };

                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();

                await _hubContext.Clients.User(proposal.DesignRequest.UserID).SendAsync("ReceiveNotification", new
                {
                    title = notification.Title,
                    message = notification.Message,
                    type = "Success",
                    relatedId = proposalId,
                    playSound = true
                });
            }

            return true;
        }
    }
}