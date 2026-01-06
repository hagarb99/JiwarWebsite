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

                // 2. Send Real-time Notification
                await _hubContext.Clients.User(request.UserID).SendAsync("ReceiveNotification", notification.Title, notification.Message);
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
                    DesignerEmail = p.Designer.User.Email
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

        public async Task<ProposalDto> ChooseProposalAsync(int proposalId, string ownerId)
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

            return _mapper.Map<ProposalDto>(selected);
        }
    }
}