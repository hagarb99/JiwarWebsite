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

        public async Task<int> GetTotalValuationsAsync()
        {
            return await _context.ValuationHistories.CountAsync();
        }

        public async Task<int> GetValuationsTodayAsync()
        {
            var today = DateTime.UtcNow.Date;
            return await _context.ValuationHistories
                .CountAsync(v => v.CreatedAt >= today);
        }

        public async Task<int> GetValuationsThisWeekAsync()
        {
            var weekAgo = DateTime.UtcNow.AddDays(-7);
            return await _context.ValuationHistories
                .CountAsync(v => v.CreatedAt >= weekAgo);
        }

        public async Task<int> GetValuationsThisMonthAsync()
        {
            var monthStart = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            return await _context.ValuationHistories
                .CountAsync(v => v.CreatedAt >= monthStart);
        }
    }

}
