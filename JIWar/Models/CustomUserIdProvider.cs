using Microsoft.AspNetCore.SignalR;

public class CustomUserIdProvider : Microsoft.AspNetCore.SignalR.IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        // افترضنا إن JWT فيه claim باسم NameIdentifier
        return connection.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
    }
}