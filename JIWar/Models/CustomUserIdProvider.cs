using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

public class CustomUserIdProvider : Microsoft.AspNetCore.SignalR.IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        // افترضنا إن JWT فيه claim باسم NameIdentifier
        return connection.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        //return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}