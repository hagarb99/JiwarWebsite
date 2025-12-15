using GEWAR;
using Google;
using Microsoft.EntityFrameworkCore;

namespace Jiwar.Repositories.User
{

    public class UserRepository : IUserRepository
    {
        private readonly GiwarContext _context;

        public UserRepository(GiwarContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalUsersAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<int> GetNewUsersTodayAsync()
        {
            var today = DateTime.UtcNow.Date;
            return await _context.Users
                .CountAsync(u => u.RegistrationDate >= today);
        }

        public async Task<int> GetNewUsersThisWeekAsync()
        {
            var startOfWeek = DateTime.UtcNow.AddDays(-7);
            return await _context.Users
                .CountAsync(u => u.RegistrationDate >= startOfWeek);
        }

        public async Task<int> GetNewUsersThisMonthAsync()
        {
            var startOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            return await _context.Users
                .CountAsync(u => u.RegistrationDate >= startOfMonth);
        }

        public async Task<int> GetActiveUsersAsync()
        {
            var lastWeek = DateTime.UtcNow.AddDays(-7);
            return await _context.Users
                .CountAsync(u => u.LockoutEnd == null);
        }


        public async Task<Dictionary<string, int>> GetUsersCountByRoleAsync()
        {
            return await _context.Users
                .GroupBy(u => u.Role)
                .Select(g => new { Role = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Role, x => x.Count);
        }
    }

}

