using Jiwar.Models.ChatAi;
using System.Threading.Tasks;

namespace Jiwar.Repositories.ChatAi
{
    public interface IChatRepository
    {
        Task AddAsync(SimulationChatMessage message);
        Task<List<SimulationChatMessage>> GetBySimulationIdAsync(int simulationId);
        Task<List<SimulationChatMessage>> GetByUserIdAsync(string userId);

    }
}