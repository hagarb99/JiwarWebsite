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

using Microsoft.Extensions.Logging;

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
        private readonly ILogger<DesignRequestController> _logger;

        public DesignRequestController(
            IDesignRequestService service, 
            IHubContext<ChatHub> chatHubContext,
            IPropertyService propertyService,
            IImgService imgService,
            UserManager<User> userManager,
            IHubContext<NotificationHub> notificationHubContext,
            INotificationService notificationService,
            ILogger<DesignRequestController> logger)
        {
            _service = service;
            _chatHubContext = chatHubContext;
            _propertyService = propertyService;
            _imgService = imgService;
            _userManager = userManager;
            _notificationHubContext = notificationHubContext;
            _notificationService = notificationService;
            _logger = logger;
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
                Console.WriteLine($"🔍 GET WORKSPACE DEBUG:");
                Console.WriteLine($"   RequestID: {id}");
                Console.WriteLine($"   UserID: {userId}");
                
                var result = await _service.GetWorkspaceAsync(id, userId);
                
                Console.WriteLine($"   PropertyID: {result?.DesignRequest?.PropertyID}");
                Console.WriteLine($"   Chat History Count: {result?.ChatHistory?.Count ?? 0}");
                Console.WriteLine($"   Designer: {result?.AcceptedProposal?.DesignerName} (ID: {result?.AcceptedProposal?.DesignerId})");
                Console.WriteLine($"   Owner: {result?.DesignRequest?.UserID}");
                
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"❌ GET WORKSPACE ERROR: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "PropertyOwner,Customer,InteriorDesigner")]
        [HttpPost("{requestId}/chat/send")]
        public async Task<IActionResult> SendWorkspaceMessage(int requestId, [FromBody] ChatMessageDTO dto)
        {
            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(senderId))
            {
                Console.WriteLine("❌ SEND MESSAGE ERROR: User not authenticated");
                return Unauthorized(new { error = "User not authenticated. Please login again." });
            }

            Console.WriteLine($"📨 SEND MESSAGE REQUEST:");
            Console.WriteLine($"   RequestID: {requestId}");
            Console.WriteLine($"   SenderID: {senderId}");
            Console.WriteLine($"   Message: {dto.MessageText ?? dto.Message}");

            try
            {
                var workspace = await _service.GetWorkspaceAsync(requestId);
                
                if (workspace == null)
                {
                    Console.WriteLine($"❌ ERROR: Workspace not found for request {requestId}");
                    return BadRequest(new { error = $"Workspace not found for request {requestId}" });
                }
                
                if (workspace.AcceptedProposal == null)
                {
                    Console.WriteLine($"❌ ERROR: No accepted proposal for request {requestId}");
                    return BadRequest(new { error = "Cannot send message. No accepted proposal found. The property owner must accept a proposal first." });
                }

                var propertyId = workspace.DesignRequest.PropertyID;
                var messageContent = dto.MessageText ?? dto.Message;

                if (string.IsNullOrEmpty(messageContent))
                {
                    Console.WriteLine("❌ ERROR: Message is empty");
                    return BadRequest(new { error = "Message cannot be empty" });
                }

                // Define receiver based on who is sending
                string receiverId = (senderId == workspace.AcceptedProposal.DesignerId) 
                    ? workspace.DesignRequest.UserID // Actual UserID of the Property Owner
                    : workspace.AcceptedProposal.DesignerId;

                if (string.IsNullOrEmpty(receiverId))
                {
                    Console.WriteLine("❌ ERROR: Could not determine receiver");
                    return BadRequest(new { error = "Could not determine message receiver" });
                }

                // 🔍 LOG: Chat Details
                Console.WriteLine($"✅ CHAT MESSAGE DEBUG:");
                Console.WriteLine($"   RequestID: {requestId}");
                Console.WriteLine($"   PropertyID: {propertyId}");
                Console.WriteLine($"   SenderID: {senderId}");
                Console.WriteLine($"   ReceiverID: {receiverId}");
                Console.WriteLine($"   DesignerID: {workspace.AcceptedProposal.DesignerId}");
                Console.WriteLine($"   OwnerID: {workspace.DesignRequest.UserID}");
                Console.WriteLine($"   SignalR Room: {propertyId}");

                // Ensure sender and receiver are different (self-messages should not be unread)
                bool isSelfMessage = senderId == receiverId;
                
                var chat = new Chat
                {
                    PropertyID = propertyId,
                    SenderID = senderId,
                    ReceiverID = receiverId, 
                    MessageText = messageContent,
                    MessageType = dto.MessageType,
                    SentDate = DateTime.UtcNow,
                    IsRead = isSelfMessage // If self-message, mark as read; otherwise false (unread)
                };

                await _propertyService.SendMessageAsync(chat);
                
                // 📊 UNREAD COUNT LOGGING
                if (!isSelfMessage)
                {
                    Console.WriteLine($"📬 Message marked as UNREAD for receiver: {receiverId}");
                }
                else
                {
                    Console.WriteLine($"📨 Self-message detected (sender == receiver), marked as READ");
                }
                Console.WriteLine("✅ Message saved to database");

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

                Console.WriteLine($"📡 Broadcasting to SignalR Group: {propertyId}");
                                                                                            _logger.LogInformation("📡 Hub SendToRoom (DesignRequest): ToGroup={GroupId}, Sender={SenderId}", propertyId, senderId);
                                                                                            await _chatHubContext.Clients.Group(propertyId.ToString()).SendAsync("ReceiveMessage", responseData);

                                                                                            // Send Notification to the Receiver (Realtime + Persistence)
                                                                                            // Only if sender != receiver (not a self-message)
                                                                                            if (!isSelfMessage)
                                                                                            {
                                                                                                try 
                                                                                                {
                                                                                                    string notTitle = sender?.Name ?? "New Message";
                                                                                                    string notMessage = messageContent.Length > 30 ? messageContent.Substring(0, 30) + "..." : messageContent;

                                                                                                    // 1. Create DB Notification
                                                                                                    await _notificationService.CreateNotificationAsync(receiverId, notTitle, notMessage, requestId.ToString(), "Chat");

                                                                                                    // 2. Get updated unread count for receiver
                                                                                                    int totalUnread = await _service.GetTotalUnreadMessagesCountAsync(receiverId);
                                                                                                    Console.WriteLine($"📊 UNREAD COUNT AFTER INSERT: Receiver={receiverId}, Count={totalUnread}");
                                                                                                    
                                                                                                    // 3. Send Realtime Chat Notification (existing event)
                                                                                                    Console.WriteLine($"🔔 Sending ReceiveChatNotification to User: {receiverId}");
                                                                                                    await _notificationHubContext.Clients.User(receiverId).SendAsync("ReceiveChatNotification", new 
                                                                                                    {
                                                                                                        title = notTitle,
                                                                                                        message = notMessage,
                                                                                                        sentDate = DateTime.Now,
                                                                                                        relatedId = requestId.ToString(),
                                                                                                        type = "Chat",
                                                                                                        unreadCount = totalUnread
                                                                                                    });

                                                                                                    // 4. Send dedicated unread count update event (NEW EVENT for badge updates)
                                                                                                    Console.WriteLine($"📢 Sending ReceiveUnreadCountUpdated to User: {receiverId}, totalUnreadCount={totalUnread}");
                                                                                                    await _notificationHubContext.Clients.User(receiverId).SendAsync("ReceiveUnreadCountUpdated", new 
                                                                                                    {
                                                                                                        totalUnreadCount = totalUnread
                                                                                                    });
                                                                                                }
                                                                                                catch (Exception ex)
                                                                                                {
                                                                                                    Console.WriteLine($"❌ Error sending notification: {ex.Message}");
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                Console.WriteLine($"⏭️ Skipping notification for self-message (sender == receiver)");
                                                                                            }

                                                                                            Console.WriteLine($"✅ Message sent successfully");
                                                                                            return Ok(new { message = "Message sent successfully", data = responseData });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ FATAL ERROR in SendWorkspaceMessage:");
                Console.WriteLine($"   Message: {ex.Message}");
                Console.WriteLine($"   StackTrace: {ex.StackTrace}");
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpGet("chat/unread-count")]
        public async Task<IActionResult> GetTotalUnreadCount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var count = await _service.GetTotalUnreadMessagesCountAsync(userId);
            return Ok(new { count });
        }

        //[HttpPost("chat/upload")]
        //public async Task<IActionResult> UploadChatFile(IFormFile file)
        //{
        //    if (file == null || file.Length == 0) return BadRequest("No file uploaded.");

        //    var fileUrl = await _imgService.SaveChatFileAsync(file);
        //    return Ok(new { url = fileUrl });
        //}

        [HttpPost("chat/mark-read/{propertyId}")]
        public async Task<IActionResult> MarkChatAsRead(int propertyId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            await _service.MarkMessagesAsReadAsync(userId, propertyId);
            return Ok(new { message = "Messages marked as read" });
        }

        // 🔍 DEBUG ENDPOINT - Remove this in production
        [HttpGet("{requestId}/debug")]
        public async Task<IActionResult> DebugWorkspace(int requestId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var workspace = await _service.GetWorkspaceAsync(requestId, userId);

                if (workspace == null)
                {
                    return Ok(new { error = "Workspace is null" });
                }

                var debugInfo = new
                {
                    currentUserId = userId,
                    designRequest = new
                    {
                        id = workspace.DesignRequest?.Id,
                        propertyID = workspace.DesignRequest?.PropertyID,
                        userID = workspace.DesignRequest?.UserID,
                        status = workspace.DesignRequest?.Status
                    },
                    acceptedProposal = workspace.AcceptedProposal == null ? null : new
                    {
                        id = workspace.AcceptedProposal.Id,
                        designerId = workspace.AcceptedProposal.DesignerId,
                        designerName = workspace.AcceptedProposal.DesignerName,
                        status = workspace.AcceptedProposal.Status
                    },
                    chatHistory = workspace.ChatHistory?.Select(c => new
                    {
                        propertyID = c.PropertyID,
                        senderID = c.SenderID,
                        receiverID = c.ReceiverID,
                        senderName = c.SenderName,
                        message = c.MessageText,
                        sentDate = c.SentDate
                    }).ToList(),
                    chatHistoryCount = workspace.ChatHistory?.Count ?? 0,
                    hasDelivered = workspace.HasDelivered,
                    hasReviewed = workspace.HasReviewed
                };

                Console.WriteLine($"🐛 DEBUG ENDPOINT CALLED:");
                Console.WriteLine($"   RequestID: {requestId}");
                Console.WriteLine($"   CurrentUserID: {userId}");
                Console.WriteLine($"   PropertyID: {workspace.DesignRequest?.PropertyID}");
                Console.WriteLine($"   OwnerID: {workspace.DesignRequest?.UserID}");
                Console.WriteLine($"   DesignerID: {workspace.AcceptedProposal?.DesignerId}");
                Console.WriteLine($"   Chat Count: {workspace.ChatHistory?.Count ?? 0}");

                return Ok(debugInfo);
            }
            catch (Exception ex)
            {
                return Ok(new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }
    }
}
