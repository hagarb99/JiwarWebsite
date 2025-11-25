using Jiwar.Models;
using Microsoft.EntityFrameworkCore;
using Jiwar.Repositories.Interfaces;
using GEWAR;
using GEWAR.Models;

namespace Jiwar.Repositories
{
    public class SubscriptionRepository : GenericRepository<Subscription>, ISubscriptionRepository
    {
        private readonly GiwarContext _context;

        public SubscriptionRepository(GiwarContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subscription>> GetAllSubscriptionsAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Subscription> GetSubscriptionByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}

