using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Jiwar.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        // Define methods that clients can call if needed
        // For now, we primarily push notifications from the server
        public async Task SendNotification(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", new 
            {
                title = user, 
                message = message,
                sentDate = System.DateTime.Now,
                playSound = true
            });
        }
    }
}
