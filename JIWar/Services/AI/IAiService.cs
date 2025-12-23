using Azure;

namespace Jiwar.Services.AI
{
    public interface IAiService
    {
        Task<string> SendAsync(string prompt, AiRequestContext context);
    }
}
