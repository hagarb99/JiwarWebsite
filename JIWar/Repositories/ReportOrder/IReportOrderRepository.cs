using Jiwar.Models;

namespace Jiwar.Repositories
{
    public interface IReportOrderRepository : IGenericRepository<ReportOrder>
    {
        Task<ReportOrder?> GetByReferenceAsync(string reference);
        Task<bool> HasUserPaidForReport(string userId, int reportId);
        Task UpdateAsync(ReportOrder order);
    }
}
