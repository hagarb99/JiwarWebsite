using Microsoft.AspNetCore.Mvc;
using Jiwar.Services.CustomerPropertyChat;
using Jiwar.DTOs.CustomerPropertyChat;
using Microsoft.AspNetCore.SignalR;
using Jiwar.Hubs;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;

namespace Jiwar.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPropertyChatController : ControllerBase
    {
        private readonly ICustomerPropertyChatService _chatService;

        public CustomerPropertyChatController(ICustomerPropertyChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto dto)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // الـ Service هنا هتحفظ في الـ DB وتبعت SignalR في نفس الوقت
                var result = await _chatService.SendMessageAsync(dto, userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory(int propertyId, string customerId = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // لو اللي بينادي المالك، لازم يبعت customerId بتاع العميل اللي بيكلمه
            // لو اللي بينادي العميل، الـ customerId هو نفسه الـ userId
            if (string.IsNullOrEmpty(customerId)) customerId = userId;

            var msgs = await _chatService.GetChatHistoryAsync(propertyId, customerId, userId);

            // بمجرد فتح التاريخ، بنعلم على الرسائل إنها مقروءة
            await _chatService.MarkAsReadAsync(propertyId, customerId, userId);

            return Ok(msgs);
        }

        [HttpGet("my-chats")] // للعملاء: عرض قائمة العقارات اللي سألوا عليها
        public async Task<IActionResult> GetMyChats()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var threads = await _chatService.GetCustomerChatsAsync(userId);
            return Ok(threads);
        }

        [HttpGet("owner-chats")] // للملاك: عرض قائمة العملاء اللي سألوا على عقاراتهم
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
            return Ok(new { count = count });
        }
    }
}