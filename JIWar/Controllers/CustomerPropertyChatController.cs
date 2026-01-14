using Microsoft.AspNetCore.Mvc;
using Jiwar.Services.CustomerPropertyChat;
using Jiwar.DTOs.CustomerPropertyChat;
using Microsoft.AspNetCore.SignalR;
using Jiwar.Hubs;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Jiwar.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPropertyChatController : ControllerBase
    {
        private readonly ICustomerPropertyChatService _chatService;
        private readonly IHubContext<CustomerPropertyChatHub> _hubContext;
        private readonly IHubContext<NotificationHub> _notificationHubContext;

        public CustomerPropertyChatController(
            ICustomerPropertyChatService chatService, 
            IHubContext<CustomerPropertyChatHub> hubContext,
            IHubContext<NotificationHub> notificationHubContext)
        {
            _chatService = chatService;
            _hubContext = hubContext;
            _notificationHubContext = notificationHubContext;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _chatService.SendMessageAsync(dto, userId);

                // Check if this is a self-message
                bool isSelfMessage = result.SenderId == result.ReceiverId;
                
                // Log message marked unread
                if (!isSelfMessage)
                {
                    Console.WriteLine($"📬 [CustomerChat] Message marked as UNREAD for receiver: {result.ReceiverId}");
                }
                else
                {
                    Console.WriteLine($"📨 [CustomerChat] Self-message detected, marked as READ");
                }

                // Determining users to notify: Sender and Receiver.
                // This ensures both parties receive the message if online.
                await _hubContext.Clients.Users(new[] { result.SenderId, result.ReceiverId }).SendAsync("ReceiveMessage", result);

                // Send ReceiveUnreadCountUpdated to the receiver (only if not self-message)
                if (!isSelfMessage)
                {
                    int totalUnread = await _chatService.GetUnreadCountAsync(result.ReceiverId);
                    Console.WriteLine($"📊 [CustomerChat] UNREAD COUNT AFTER INSERT: Receiver={result.ReceiverId}, Count={totalUnread}");
                    
                    Console.WriteLine($"📢 [CustomerChat] Sending ReceiveUnreadCountUpdated to User: {result.ReceiverId}, totalUnreadCount={totalUnread}");
                    await _notificationHubContext.Clients.User(result.ReceiverId).SendAsync("ReceiveUnreadCountUpdated", new 
                    {
                        totalUnreadCount = totalUnread
                    });
                }
                   
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ [CustomerChat] Error sending message: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory(int propertyId, string customerId = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(string.IsNullOrEmpty(customerId)) customerId = userId;
            
            var msgs = await _chatService.GetChatHistoryAsync(propertyId, customerId, userId);
            
            await _chatService.MarkAsReadAsync(propertyId, customerId, userId);
            
            return Ok(msgs);
        }

        [HttpGet("my-chats")] // For Customers
        public async Task<IActionResult> GetMyChats()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var threads = await _chatService.GetCustomerChatsAsync(userId);
            return Ok(threads);
        }

        [HttpGet("owner-chats")] // For Owners
        public async Task<IActionResult> GetOwnerChats()
        {
             var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
             var threads = await _chatService.GetOwnerChatsAsync(userId);
             return Ok(threads);
        }

        [HttpGet("unread-count")]
        public async Task<IActionResult> GetUnreadCount()
        {
             var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
             var count = await _chatService.GetUnreadCountAsync(userId);
             return Ok(new { Count = count });
        }
    }
}
