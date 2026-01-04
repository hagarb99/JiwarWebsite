using Jiwar.Models.ChatAi;

namespace Jiwar.Services.AI.Chat
{
    public interface IAiChatService
    {
        Task<string> StartChatAsync(int simulationId);

        Task<string> SendMessageAsync(
            int simulationId,
            string userId,
            string userMessage);

        Task SaveMessageAsync (SimulationChatMessage chatMessage);
        Task<List<SimulationChatMessage>> GetUserMessagesAsync(string userId);



    }
}
