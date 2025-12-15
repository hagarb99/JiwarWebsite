using Jiwar.Models;
using Jiwar.Repositories;

public interface IReportOrderRepository : IGenericRepository<ReportOrder>
{
    Task<ReportOrder?> GetByReferenceAsync(string reference);
    Task<bool> HasUserPaidForReport(string userId, int reportId);
    Task UpdateAsync(ReportOrder order);

    // 🔥 Admin Analytics
    Task<decimal> GetTotalRevenueAsync();
    Task<decimal> GetRevenueTodayAsync();
    Task<decimal> GetRevenueThisWeekAsync();
    Task<decimal> GetRevenueThisMonthAsync();


namespace Jiwar.Repositories
{
    public interface IReportOrderRepository : IGenericRepository<ReportOrder>
    {
        Task<ReportOrder?> GetByReferenceAsync(string reference);
        Task<bool> HasUserPaidForReport(string userId, int reportId);
        Task UpdateAsync(ReportOrder order);
        Task<ReportOrder?> GetByOrderIdAsync(long orderId);
        Task<ReportOrder?> GetByReportIdAsync(int reportId);

    }
}
