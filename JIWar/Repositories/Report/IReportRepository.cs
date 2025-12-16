using GEWAR.Models;

namespace Jiwar.Repositories
{
    public interface IReportRepository : IGenericRepository<Report>
    {
        Task<IEnumerable<Report>> GetReportsByUserAsync(string userId);


    }
}
