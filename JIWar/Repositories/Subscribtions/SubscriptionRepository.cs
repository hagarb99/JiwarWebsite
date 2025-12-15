using GEWAR;
using GEWAR.Models;
using Jiwar.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Jiwar.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly GiwarContext _context;

        public SubscriptionRepository(GiwarContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.Subscriptions
                .Where(s => s.statusEnum2 == StatusEnum2.Active)
                .SumAsync(s => s.Price);
        }

        public async Task<decimal> GetTodayRevenueAsync()
        {
            var today = DateTime.UtcNow.Date;

            return await _context.Subscriptions
                .Where(s =>
                    s.statusEnum2 == StatusEnum2.Active &&
                    s.StartDate.Date == today)
                .SumAsync(s => s.Price);
        }

        public async Task<decimal> GetWeekRevenueAsync()
        {
            var lastWeek = DateTime.UtcNow.AddDays(-7);

            return await _context.Subscriptions
                .Where(s =>
                    s.statusEnum2 == StatusEnum2.Active &&
                    s.StartDate >= lastWeek)
                .SumAsync(s => s.Price);
        }

        public async Task<decimal> GetMonthRevenueAsync()
        {
            var startOfMonth = new DateTime(
                DateTime.UtcNow.Year,
                DateTime.UtcNow.Month,
                1);

            return await _context.Subscriptions
                .Where(s =>
                    s.statusEnum2 == StatusEnum2.Active &&
                    s.StartDate >= startOfMonth)
                .SumAsync(s => s.Price);
        }
    }
}
