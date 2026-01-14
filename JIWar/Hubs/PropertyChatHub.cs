using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Jiwar.Hubs
{
    public class PropertyChatHub : Hub
    {
        // دالة لجعل المستخدم ينضم لغرفة خاصة به بناءً على الـ UserId
        // ده بيسهل إرسال رسائل لمستخدم معين
        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }
            await base.OnConnectedAsync();
        }
        public async Task JoinPropertyChat(int propertyId, string customerId)
        {
            // اسم الغرفة يكون مميز للعقار والعميل (عشان المالك ممكن يكلم 10 عملاء على نفس العقار)
            string roomName = $"Chat_{propertyId}_{customerId}";
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }
    }
}
