using GEWAR;
using Jiwar.Models.ChatAi;
using Microsoft.EntityFrameworkCore;

namespace Jiwar.Repositories.ChatAi
{
    public class QuotaRepository : IQuotaRepository
    {
        private readonly GiwarContext _context;
        public QuotaRepository(GiwarContext context) => _context = context;

        public async Task<UserChatQuota?> GetByUserIdAsync(string userId)
        {
            return await _context.UserChatQuotas.FirstOrDefaultAsync(q => q.UserId == userId);
        }

        public async Task AddAsync(UserChatQuota quota)
        {
            _context.UserChatQuotas.Add(quota);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserChatQuota quota)
        {
            _context.UserChatQuotas.Update(quota);
            await _context.SaveChangesAsync();
        }
    }
}
