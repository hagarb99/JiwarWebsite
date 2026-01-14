using Jiwar.DTOs.CustomerPropertyChat;
using Jiwar.Models.CustomerPropertyChat;
using Jiwar.Models; 
using GEWAR; // Context
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Linq; // Add this
using System.Collections.Generic; // Add this
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Jiwar.Hubs; // Add this

namespace Jiwar.Services.CustomerPropertyChat
{
    public interface ICustomerPropertyChatService
    {
        Task<CustomerMessageDto> SendMessageAsync(SendMessageDto dto, string senderId, bool isSystemInitiated = false);
        Task<List<CustomerMessageDto>> GetChatHistoryAsync(int propertyId, string customerId, string currentUserId);
        Task MarkAsReadAsync(int propertyId, string customerId, string readerId);
        Task<int> GetUnreadCountAsync(string userId);
        Task<List<ChatThreadDto>> GetOwnerChatsAsync(string ownerId);
        Task<List<ChatThreadDto>> GetCustomerChatsAsync(string customerId);
        Task InitializeChatOnAcceptAsync(int propertyId, string customerId, string ownerId);


    }

    public class CustomerPropertyChatService : ICustomerPropertyChatService
    {
        private readonly GiwarContext _context;
        private readonly IHubContext<CustomerPropertyChatHub> _hubContext;
        public CustomerPropertyChatService(GiwarContext context,
            IHubContext<CustomerPropertyChatHub> _hubContext
            )
        {
            _context = context;
           this._hubContext = _hubContext;
        }

        public async Task<CustomerMessageDto> SendMessageAsync(SendMessageDto dto, string senderId, bool isSystemInitiated = false)
        {
            var property = await _context.Properties.FindAsync(dto.PropertyId);
            if (property == null) throw new Exception("Property not found");

            var isSenderOwner = property.OwnerID == senderId;
            string receiverId;

            if (isSenderOwner)
            {
                if (string.IsNullOrEmpty(dto.ReceiverId)) throw new Exception("ReceiverId required for owner reply");
                receiverId = dto.ReceiverId;

                if (!isSystemInitiated)
                {
                    bool exists = await _context.CustomerPropertyMessages.AnyAsync(m => m.PropertyId == dto.PropertyId && (m.SenderId == receiverId || m.ReceiverId == receiverId));
                    if (!exists) throw new Exception("Owner cannot initiate chat");
                }
            }
            else
            {
                receiverId = property.OwnerID;
            }

            var msg = new CustomerPropertyMessage
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                PropertyId = dto.PropertyId,
                MessageText = dto.MessageText,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            _context.CustomerPropertyMessages.Add(msg);
            await _context.SaveChangesAsync();

            var senderUser = await _context.Users.FindAsync(senderId);

            var resultDto = new CustomerMessageDto
            {
                Id = msg.Id,
                SenderId = msg.SenderId,
                SenderName = senderUser?.UserName ?? "Unknown",
                ReceiverId = msg.ReceiverId,
                PropertyId = msg.PropertyId,
                MessageText = msg.MessageText,
                CreatedAt = msg.CreatedAt,
                IsRead = msg.IsRead,
                IsMine = false // ستتغير في الـ Frontend بناءً على الـ ID
            };

            // --- التعديل الجوهري هنا لضمان عمل الـ SignalR ---

            // تحديد من هو العميل في هذه المحادثة لتحديد اسم الغرفة الصحيح
            string chatCustomerId = isSenderOwner ? receiverId : senderId;
            string groupName = $"PropertyChat_{dto.PropertyId}_{chatCustomerId}";

            // الإرسال للمجموعة بالكامل (المالك والعميل المنضمين للغرفة)
            await _hubContext.Clients.Group(groupName).SendAsync("ReceiveMessage", resultDto);

            resultDto.IsMine = true; // للمرسل تكون true
            return resultDto;
        }
        public async Task<List<CustomerMessageDto>> GetChatHistoryAsync(int propertyId, string customerId, string currentUserId)
        {
            var msgs = await _context.CustomerPropertyMessages
                .Where(m => m.PropertyId == propertyId && (m.SenderId == customerId || m.ReceiverId == customerId))
                .OrderBy(m => m.CreatedAt)
                .Include(m => m.Sender)
                .ToListAsync();

            return msgs.Select(m => new CustomerMessageDto
            {
                 Id = m.Id,
                 SenderId = m.SenderId,
                 SenderName = m.Sender.UserName,
                 SenderProfilePicURL = m.Sender.ProfilePicURL,
                 ReceiverId = m.ReceiverId,
                 PropertyId = m.PropertyId,
                 MessageText = m.MessageText,
                 CreatedAt = m.CreatedAt,
                 IsRead = m.IsRead,
                 IsMine = m.SenderId == currentUserId
            }).ToList();
        }

        public async Task MarkAsReadAsync(int propertyId, string customerId, string readerId)
        {
            var unread = await _context.CustomerPropertyMessages
                .Where(m => m.PropertyId == propertyId && 
                            (m.SenderId == customerId || m.ReceiverId == customerId) &&
                            m.ReceiverId == readerId && 
                            !m.IsRead)
                .ToListAsync();

            if (unread.Any())
            {
                foreach (var msg in unread)
                {
                    msg.IsRead = true;
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetUnreadCountAsync(string userId)
        {
            return await _context.CustomerPropertyMessages
                .Where(m => m.ReceiverId == userId && !m.IsRead)
                .CountAsync();
        }

        public async Task<List<ChatThreadDto>> GetOwnerChatsAsync(string ownerId)
        {
            var raw = await _context.CustomerPropertyMessages
                .Where(m => m.ReceiverId == ownerId || m.SenderId == ownerId)
                .Include(m => m.Property)
                    .ThenInclude(p => p.PropertyMedia)
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .ToListAsync();

            var grouped = raw
                .GroupBy(m => new { 
                    PropId = m.PropertyId, 
                    CustId = m.SenderId == ownerId ? m.ReceiverId : m.SenderId 
                })
                .Select(g => new {
                    Key = g.Key,
                    LastMsg = g.OrderByDescending(x => x.CreatedAt).First(),
                    Unread = g.Count(x => x.ReceiverId == ownerId && !x.IsRead)
                })
                .OrderByDescending(x => x.LastMsg.CreatedAt)
                .ToList();

            var result = new List<ChatThreadDto>();
            foreach(var item in grouped)
            {
                 var last = item.LastMsg;
                 var otherUser = last.SenderId == ownerId ? last.Receiver : last.Sender;
                 
                 result.Add(new ChatThreadDto
                 {
                     PropertyId = item.Key.PropId,
                     PropertyTitle = last.Property?.Title ?? "Unknown Property",
                     PropertyImage = last.Property?.PropertyMedia?.FirstOrDefault()?.MediaURL ?? "",
                     CustomerId = item.Key.CustId,
                     CustomerName = otherUser?.UserName ?? "Unknown",
                     CustomerImage = otherUser?.ProfilePicURL ?? "",
                     LastMessage = last.MessageText,
                     LastMessageDate = last.CreatedAt,
                     UnreadCount = item.Unread
                 });
            }
            return result;
        }

        public async Task<List<ChatThreadDto>> GetCustomerChatsAsync(string customerId)
        {
             var raw = await _context.CustomerPropertyMessages
                .Where(m => m.ReceiverId == customerId || m.SenderId == customerId)
                .Include(m => m.Property)
                    .ThenInclude(p => p.PropertyMedia)
                .Include(m => m.Property)
                    .ThenInclude(p => p.OwnerUser)
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .ToListAsync();

             var grouped = raw
                .GroupBy(m => m.PropertyId)
                .Select(g => new {
                    PropId = g.Key,
                    LastMsg = g.OrderByDescending(x => x.CreatedAt).First(),
                    Unread = g.Count(x => x.ReceiverId == customerId && !x.IsRead)
                })
                .OrderByDescending(x => x.LastMsg.CreatedAt)
                .ToList();

             var result = new List<ChatThreadDto>();
             foreach(var item in grouped)
             {
                 var last = item.LastMsg;
                 
                 result.Add(new ChatThreadDto
                 {
                     PropertyId = item.PropId,
                     PropertyTitle = last.Property?.Title ?? "Unknown",
                     PropertyImage = last.Property?.PropertyMedia?.FirstOrDefault()?.MediaURL ?? "",
                     CustomerId = customerId,
                     CustomerName = last.Property?.OwnerUser?.UserName ?? "Owner", 
                     CustomerImage = last.Property?.OwnerUser?.ProfilePicURL ?? "",
                     LastMessage = last.MessageText,
                     LastMessageDate = last.CreatedAt,
                     UnreadCount = item.Unread
                 });
             }
             return result;
        }
        public async Task InitializeChatOnAcceptAsync(int propertyId, string customerId, string ownerId)
        {
            // رسالة ترحيبية تلقائية تفتح الشات
            var welcomeMsg = new SendMessageDto
            {
                PropertyId = propertyId,
                ReceiverId = customerId, // المالك بيبعت للعميل
                MessageText = "لقد تم قبول طلب المعاينة الخاص بك. يمكنك الآن التواصل مع المالك."
            };

            // نستخدم الدالة اللي عندنا فعلاً لإرسال أول رسالة
            // ملاحظة: لازم نشيل شرط (exists) من SendMessageAsync في حالة إن النظام هو اللي بيبدأ
            await SendMessageAsync(welcomeMsg, ownerId, true);
        }

    }
}
