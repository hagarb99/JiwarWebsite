using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Jiwar.Hubs
{
    public class ChatHub : Hub
    {
        // Send message to a specific user
        public async Task SendMessage(string receiverId, string senderId, string message)
        {
            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, message);
        }

        // Optional: broadcast to all users
        public async Task BroadcastMessage(string senderId, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", senderId, message);
        }
    }
}
