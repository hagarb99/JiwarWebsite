using Microsoft.EntityFrameworkCore;
using GEWAR;
using GEWAR.Models;

namespace Jiwar.Repositories
{
    public class ReportRepository : GenericRepository<Report>, IReportRepository

    {
        private readonly GiwarContext _context;

        public ReportRepository(GiwarContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Report>> GetReportsByUserAsync(string userId)
        {
            return await _context.Reports
                .Where(r => r.UserID == userId && !r.IsDeleted)
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync();
        }

    }
}
