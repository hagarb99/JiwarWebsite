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

        public CustomerPropertyChatController(ICustomerPropertyChatService chatService, IHubContext<CustomerPropertyChatHub> hubContext)
        {
            _chatService = chatService;
            _hubContext = hubContext;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _chatService.SendMessageAsync(dto, userId);

                // Determining users to notify: Sender and Receiver.
                // This ensures both parties receive the message if online.
                   
                await _hubContext.Clients.Users(new[] { result.SenderId, result.ReceiverId }).SendAsync("ReceiveMessage", result);
                   
                return Ok(result);
            }
            catch (Exception ex)
            {
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
