using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Jiwar.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("✅ SignalR Connected: ConnectionId={ConnectionId}, UserId={UserId}", Context.ConnectionId, Context.UserIdentifier);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogInformation("❌ SignalR Disconnected: ConnectionId={ConnectionId}, UserId={UserId}, Error={Error}", Context.ConnectionId, Context.UserIdentifier, exception?.Message);
            await base.OnDisconnectedAsync(exception);
        }

        // Join a specific property chat room
        public async Task JoinChat(string propertyId)
        {
            _logger.LogInformation("📥 JoinChat: ConnectionId={ConnectionId} joining Group={GroupId}", Context.ConnectionId, propertyId);
            await Groups.AddToGroupAsync(Context.ConnectionId, propertyId);
        }

        // Leave a specific property chat room
        public async Task LeaveChat(string propertyId)
        {
            _logger.LogInformation("📤 LeaveChat: ConnectionId={ConnectionId} leaving Group={GroupId}", Context.ConnectionId, propertyId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, propertyId);
        }

        // Send message to a specific user (fallback)
        public async Task SendMessage(string receiverId, string senderId, string message)
        {
            _logger.LogInformation("📡 Hub SendMessage: ToUser={ReceiverId}, From={SenderId}", receiverId, senderId);
            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, message);
        }

        // Send message to the property group
        public async Task SendToRoom(string propertyId, string senderId, string message)
        {
            _logger.LogInformation("📡 Hub SendToRoom: ToGroup={GroupId}, From={SenderId}", propertyId, senderId);
            await Clients.Group(propertyId).SendAsync("ReceiveMessage", senderId, message);
        }
    }
}
