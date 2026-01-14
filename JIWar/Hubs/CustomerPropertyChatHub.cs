using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Jiwar.Hubs
{
    public class CustomerPropertyChatHub : Hub
    {
        public async Task JoinChat(int propertyId, string customerId)
        {
            var groupName = $"PropertyChat_{propertyId}_{customerId}";
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveChat(int propertyId, string customerId)
        {
            var groupName = $"PropertyChat_{propertyId}_{customerId}";
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
