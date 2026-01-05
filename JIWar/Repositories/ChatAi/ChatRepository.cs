using GEWAR;
using Jiwar.Models.ChatAi;
using Microsoft.EntityFrameworkCore;

namespace Jiwar.Repositories.ChatAi
{
    public class ChatRepository
    {
        private readonly GiwarContext _context;

        public ChatRepository(GiwarContext context)
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

        public async Task<List<SimulationChatMessage>> GetByUserIdAsync(string userId)
        {
            return await _context.SimulationChatMessages
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}
