using Microsoft.EntityFrameworkCore;

using GEWAR;

using Jiwar.Models.Valuation;

namespace Jiwar.Repositories.Valuation
{
    public class ValuationHistoryRepository : IValuationHistoryRepository
    {
        private readonly GiwarContext _context;

        public ValuationHistoryRepository(GiwarContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(ValuationHistory history)
        {
            await _context.ValuationHistories.AddAsync(history);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ValuationHistory>> GetByUserAsync(string userId)
        {
            return await _context.ValuationHistories
                .Where(v => v.UserId == userId)
                .OrderByDescending(v => v.CreatedAt)
                .ToListAsync();
        }
    }
}
