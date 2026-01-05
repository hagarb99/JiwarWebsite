using GEWAR;
using Jiwar.Models.ChatAi;
using Microsoft.EntityFrameworkCore;

namespace Jiwar.Repositories.SimulationChatAI
{
    public class SimulationChatRepository : ISimulationChatRepository
    {
        private readonly GiwarContext _context;

        public SimulationChatRepository(GiwarContext context)
        {
            _context = context;
        }
        public async Task AddAsync(SimulationChatMessage message)
        {
            _context.SimulationChatMessages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SimulationChatMessage>> GetBySimulationIdAsync(int simulationId)
        {
            return await _context.SimulationChatMessages
                .Where(x => x.RenovationSimulationID == simulationId)
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<SimulationChatMessage>> GetRecentAsync(int simulationId, int limit = 20)
        {
            return await _context.SimulationChatMessages
                 .Where(x => x.RenovationSimulationID == simulationId)
                 .OrderByDescending(x => x.CreatedAt)
                 .Take(limit)
                 .OrderBy(x => x.CreatedAt)
                 .ToListAsync();
        }

        public async Task<List<SimulationChatMessage>> GetByUserIdAsync(string userId)
        {
            return await _context.SimulationChatMessages
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();
        }

    }
}
