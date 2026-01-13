using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Jiwar.Hubs
{
    public class ChatHub : Hub
    {
        // Join a specific property chat room
        public async Task JoinChat(string propertyId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, propertyId);
        }

        // Leave a specific property chat room
        public async Task LeaveChat(string propertyId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, propertyId);
        }

        // Send message to a specific user (fallback)
        public async Task SendMessage(string receiverId, string senderId, string message)
        {
            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, message);
        }

        // Send message to the property group
        public async Task SendToRoom(string propertyId, string senderId, string message)
        {
            await Clients.Group(propertyId).SendAsync("ReceiveMessage", senderId, message);
        }
    }
}
