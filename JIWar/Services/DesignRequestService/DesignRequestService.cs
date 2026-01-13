using AutoMapper;
using GEWAR;
using Jiwar.DTOs.DesignDto;
using Jiwar.Models;
using Microsoft.EntityFrameworkCore;
using GEWAR.Models;
using Jiwar.DTOs.ChatDTOs;

namespace Jiwar.Services.DesignRequestService
{
    public class DesignRequestService : IDesignRequestService
    {
        private readonly GiwarContext _context;
        private readonly IMapper _mapper;

        public DesignRequestService(GiwarContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DesignRequestDto> CreateDesignRequestAsync(string userId, DesignRequestDto dto)
        {
            var request = _mapper.Map<DesignRequest>(dto);
            request.UserID = userId;
            request.Status = "Open";
            request.CreatedAt = DateTime.UtcNow;

            _context.DesignRequests.Add(request);
            await _context.SaveChangesAsync();

            var dtoResult = _mapper.Map<DesignRequestDto>(request);
            return dtoResult;
        }

        public async Task<List<DesignRequestDto>> GetUserRequestsAsync(string userId)
        {
            var requests = await _context.DesignRequests
                .Include(r => r.Proposals)
                .Where(r => r.UserID == userId)
                .ToListAsync();

            return _mapper.Map<List<DesignRequestDto>>(requests);
        }

        public async Task<List<DesignRequestDto>> GetAvailableRequestsAsync()
        {
            var requests = await _context.DesignRequests
                .Include(r => r.Proposals)
                .Where(r => r.Status == "Open" || r.Status == "HasProposals")
                .ToListAsync();

            return _mapper.Map<List<DesignRequestDto>>(requests);
        }

        public async Task<DesignRequestDto> GetRequestByIdAsync(int id)
        {
            var request = await _context.DesignRequests
                .Include(r => r.Proposals)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null) return null;

            return _mapper.Map<DesignRequestDto>(request);
        }

        public async Task UpdateRequestStatusAsync(int requestId, string status)
        {
            var request = await _context.DesignRequests
                .Include(r => r.Proposals)
                .FirstOrDefaultAsync(r => r.Id == requestId);

            if (request == null) return;

            request.Status = status;

            if (status == "Completed" || status == "Delivered")
            {
                var acceptedProposal = request.Proposals
                    .FirstOrDefault(p => p.StatusEnumReq == StatusEnumReqPro.Accepted);
                
                if (acceptedProposal != null)
                {
                    acceptedProposal.StatusEnumReq = StatusEnumReqPro.Delivered;
                    if (acceptedProposal.DeliveredAt == null)
                        acceptedProposal.DeliveredAt = DateTime.UtcNow;
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<WorkspaceDto> GetWorkspaceAsync(int requestId, string currentUserId = null)
        {
            var request = await _context.DesignRequests
                .Include(r => r.Proposals)
                .ThenInclude(p => p.Designer)
                .ThenInclude(d => d.User)
                .FirstOrDefaultAsync(r => r.Id == requestId);

            if (request == null) throw new Exception("Design request not found");

            var acceptedProposal = request.Proposals
                .FirstOrDefault(p => p.StatusEnumReq == StatusEnumReqPro.Accepted || p.StatusEnumReq == StatusEnumReqPro.Delivered);

            var result = new WorkspaceDto
            {
                DesignRequest = _mapper.Map<DesignRequestDto>(request),
                HasDelivered = acceptedProposal?.StatusEnumReq == StatusEnumReqPro.Delivered,
                HasReviewed = await _context.DesignerReviews.AnyAsync(r => r.DesignRequestID == requestId)
            };

            if (acceptedProposal != null)
            {
                result.AcceptedProposal = new AcceptedProposalDto
                {
                    Id = acceptedProposal.Id,
                    DesignerId = acceptedProposal.DesignerID,
                    DesignerName = acceptedProposal.Designer?.User?.Name,
                    EstimatedCost = acceptedProposal.EstimatedCost,
                    EstimatedDays = acceptedProposal.EstimatedDays,
                    Status = (int)acceptedProposal.StatusEnumReq,
                    DeliveredAt = acceptedProposal.DeliveredAt
                };

                var ownerId = request.UserID;
                var designerId = acceptedProposal.DesignerID;
                var propertyId = request.PropertyID;

                var chats = await _context.Chats
                    .Include(c => c.Sender)
                    .Include(c => c.Receiver)
                    .Where(c => c.PropertyID == propertyId &&
                           ((c.SenderID == ownerId && c.ReceiverID == designerId) ||
                            (c.SenderID == designerId && c.ReceiverID == ownerId)))
                    .OrderBy(c => c.SentDate)
                    .ToListAsync();

                // Auto-mark messages as read when workspace is opened
                if (!string.IsNullOrEmpty(currentUserId))
                {
                    var unreadMessages = chats.Where(c => c.ReceiverID == currentUserId && !c.IsRead).ToList();
                    if (unreadMessages.Any())
                    {
                        foreach (var msg in unreadMessages)
                        {
                            msg.IsRead = true;
                        }
                        await _context.SaveChangesAsync();
                    }
                }

                result.ChatHistory = chats.Select(c => new ChatMessageDTO
                {
                    PropertyID = c.PropertyID,
                    SenderID = c.SenderID,
                    ReceiverID = c.ReceiverID,
                    SenderName = c.Sender?.Name,
                    SenderPhoto = c.Sender?.ProfilePicURL,
                    Message = c.MessageText,
                    MessageText = c.MessageText,
                    MessageType = c.MessageType,
                    SentDate = c.SentDate
                }).ToList();
            }

            return result;
        }

        public async Task<List<Jiwar.DTOs.ChatDTOs.ConversationDTO>> GetUserConversationsAsync(string userId)
        {
            var userChatsGroups = await _context.Chats
                .Include(c => c.Sender)
                .Include(c => c.Receiver)
                .Where(c => c.SenderID == userId || c.ReceiverID == userId)
                .GroupBy(c => c.PropertyID)
                .ToListAsync();

            var result = new List<Jiwar.DTOs.ChatDTOs.ConversationDTO>();

            foreach (var group in userChatsGroups)
            {
                var latestChat = group.OrderByDescending(c => c.SentDate).First();
                var unreadCount = group.Count(c => c.ReceiverID == userId && !c.IsRead);

                var isSender = latestChat.SenderID == userId;
                var otherUser = isSender ? latestChat.Receiver : latestChat.Sender;

                var designRequest = await _context.DesignRequests
                    .OrderByDescending(r => r.CreatedAt)
                    .FirstOrDefaultAsync(r => r.PropertyID == latestChat.PropertyID);

                result.Add(new Jiwar.DTOs.ChatDTOs.ConversationDTO
                {
                    PropertyID = latestChat.PropertyID,
                    DesignRequestID = designRequest?.Id ?? 0,
                    OtherUserID = otherUser?.Id ?? string.Empty,
                    OtherUserName = otherUser?.Name ?? "Unknown",
                    OtherUserPhoto = otherUser?.ProfilePicURL ?? string.Empty,
                    LastMessage = latestChat.MessageText,
                    LastMessageType = latestChat.MessageType,
                    LastMessageDate = latestChat.SentDate,
                    UnreadCount = unreadCount
                });
            }

            return result.OrderByDescending(x => x.LastMessageDate).ToList();
        }

        public async Task MarkMessagesAsReadAsync(string userId, int propertyId)
        {
            var unreadMessages = await _context.Chats
                .Where(c => c.PropertyID == propertyId && c.ReceiverID == userId && !c.IsRead)
                .ToListAsync();

            if (unreadMessages.Any())
            {
                foreach (var msg in unreadMessages)
                {
                    msg.IsRead = true;
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetTotalUnreadMessagesCountAsync(string userId)
        {
            return await _context.Chats
                .CountAsync(c => c.ReceiverID == userId && !c.IsRead);
        }
    }
}
