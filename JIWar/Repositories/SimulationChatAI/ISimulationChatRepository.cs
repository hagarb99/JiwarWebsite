using GEWAR.Models;
using Jiwar.Models.ChatAi;
using System.Threading.Tasks;

namespace Jiwar.Repositories.SimulationChatAI
{
    public interface ISimulationChatRepository
    {
        Task AddAsync(SimulationChatMessage message);

        Task<List<SimulationChatMessage>> GetBySimulationIdAsync(
            int simulationId);

        Task<List<SimulationChatMessage>> GetRecentAsync(
            int simulationId,
            int limit = 20);


        Task<List<SimulationChatMessage>> GetByUserIdAsync(string userId);

    }
}
