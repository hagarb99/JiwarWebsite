using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Jiwar.Hubs
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            // Use the same claim as the original implementation to ensure compatibility
            return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}