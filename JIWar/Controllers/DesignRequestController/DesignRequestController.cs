using Jiwar.DTOs.DesignDto;
using Jiwar.Services.DesignRequestService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using Jiwar.Hubs;
using Jiwar.DTOs.ChatDTOs;
using GEWAR.Models;
using Jiwar.Service;
using Jiwar.Services;
using Microsoft.AspNetCore.Identity;
using Jiwar.Services.NotificationService;

namespace Jiwar.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DesignRequestController : ControllerBase
    {
        private readonly IDesignRequestService _service;
        private readonly IHubContext<ChatHub> _chatHubContext;
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly IPropertyService _propertyService;
        private readonly IImgService _imgService;
        private readonly INotificationService _notificationService;
        private readonly UserManager<User> _userManager;

        public DesignRequestController(
            IDesignRequestService service, 
            IHubContext<ChatHub> chatHubContext,
            IPropertyService propertyService,
            IImgService imgService,
            UserManager<User> userManager,
            IHubContext<NotificationHub> notificationHubContext,
            INotificationService notificationService)
        {
            _service = service;
            _chatHubContext = chatHubContext;
            _propertyService = propertyService;
            _imgService = imgService;
            _userManager = userManager;
            _notificationHubContext = notificationHubContext;
            _notificationService = notificationService;
        }

        [Authorize(Roles = "PropertyOwner,Customer,InteriorDesigner")]
        [HttpGet("my-conversations")]
        public async Task<IActionResult> GetUserConversations()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var result = await _service.GetUserConversationsAsync(userId);
            return Ok(result);
        }

        [Authorize(Roles = "PropertyOwner,Customer")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateDesignRequest([FromBody] DesignRequestDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var result = await _service.CreateDesignRequestAsync(userId, dto);
            return Ok(result);
        }

        [Authorize(Roles = "PropertyOwner,Customer")]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyRequests()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var result = await _service.GetUserRequestsAsync(userId);
            return Ok(result);
        }



        [Authorize(Roles = "InteriorDesigner")]
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableRequests()
        {
            var result = await _service.GetAvailableRequestsAsync();
            return Ok(result);
        }


        [Authorize(Roles = "PropertyOwner,Customer,InteriorDesigner")]

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequestById(int id)
        {
            var result = await _service.GetRequestByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}/workspace")]
        public async Task<IActionResult> GetWorkspace(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _service.GetWorkspaceAsync(id, userId);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{requestId}/chat/send")]
        public async Task<IActionResult> SendWorkspaceMessage(int requestId, [FromBody] ChatMessageDTO dto)
        {
            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(senderId)) return Unauthorized();

            var workspace = await _service.GetWorkspaceAsync(requestId);
            if (workspace == null || workspace.AcceptedProposal == null) 
                return BadRequest("No active workspace found for this request.");

            var propertyId = workspace.DesignRequest.PropertyID;
            var messageContent = dto.MessageText ?? dto.Message;

            if (string.IsNullOrEmpty(messageContent)) return BadRequest("Message cannot be empty");

            // Define receiver based on who is sending
            string receiverId = (senderId == workspace.AcceptedProposal.DesignerId) 
                ? workspace.DesignRequest.UserID // Actual UserID of the Property Owner
                : workspace.AcceptedProposal.DesignerId;

            if (string.IsNullOrEmpty(receiverId)) 
                return BadRequest("Could not determine message receiver.");

            var chat = new Chat
            {
                PropertyID = propertyId,
                SenderID = senderId,
                ReceiverID = receiverId, 
                MessageText = messageContent,
                MessageType = dto.MessageType,
                SentDate = DateTime.UtcNow
            };

            await _propertyService.SendMessageAsync(chat);

            var sender = await _userManager.FindByIdAsync(senderId);

            // Broadcast to the Room (Chat history/update)
            var responseData = new ChatMessageDTO
            {
                PropertyID = propertyId,
                SenderID = senderId,
                ReceiverID = receiverId,
                SenderName = sender?.Name,
                SenderPhoto = sender?.ProfilePicURL,
                Message = messageContent,
                MessageText = messageContent,
                MessageType = chat.MessageType,
                SentDate = chat.SentDate
            };

            await _chatHubContext.Clients.Group(propertyId.ToString()).SendAsync("ReceiveMessage", responseData);

            // Send Notification to the Receiver (Realtime + Persistence)
            // Fix: Send only to receiverId, not broadcast.
            try 
            {
                string notTitle = sender?.Name ?? "New Message";
                string notMessage = messageContent.Length > 30 ? messageContent.Substring(0, 30) + "..." : messageContent;

                // 1. Create DB Notification
                // We use requestId as RelatedId so frontend knows which workspace to open
                await _notificationService.CreateNotificationAsync(receiverId, notTitle, notMessage, requestId.ToString(), "Chat");

                // 2. Send Realtime Notification
                int totalUnread = await _service.GetTotalUnreadMessagesCountAsync(receiverId);
                await _notificationHubContext.Clients.User(receiverId).SendAsync("ReceiveChatNotification", new 
                {
                    title = notTitle,
                    message = notMessage,
                    sentDate = DateTime.Now,
                    relatedId = requestId.ToString(),
                    type = "Chat",
                    unreadCount = totalUnread
                });
            }
            catch (Exception ex)
            {
                 // Log error but don't fail the message sending
                 Console.WriteLine($"Error sending notification: {ex.Message}");
            }

            return Ok(new { message = "Message sent successfully", data = responseData });
        }

        [HttpGet("chat/unread-count")]
        public async Task<IActionResult> GetTotalUnreadCount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var count = await _service.GetTotalUnreadMessagesCountAsync(userId);
            return Ok(new { count });
        }

        [HttpPost("chat/upload")]
        public async Task<IActionResult> UploadChatFile(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("No file uploaded.");

            var fileUrl = await _imgService.SaveChatFileAsync(file);
            return Ok(new { url = fileUrl });
        }

        [HttpPost("chat/mark-read/{propertyId}")]
        public async Task<IActionResult> MarkChatAsRead(int propertyId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            await _service.MarkMessagesAsReadAsync(userId, propertyId);
            return Ok(new { message = "Messages marked as read" });
        }
    }
}
