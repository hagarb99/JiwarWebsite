using GEWAR;
using GEWAR.Models;
using Jiwar.Models;
using Microsoft.EntityFrameworkCore;

namespace Jiwar.Repositories
{
    public class ReportOrderRepository
    : GenericRepository<ReportOrder>, IReportOrderRepository
    {
        private readonly GiwarContext _context;

        public ReportOrderRepository(GiwarContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ReportOrder?> GetByReferenceAsync(string reference)
        {
            return await _context.ReportOrders
                .FirstOrDefaultAsync(r => r.PaymentReference == reference);
        }

        public async Task<bool> HasUserPaidForReport(string userId, int reportId)
        {
            return await _context.ReportOrders.AnyAsync(r =>
                r.UserID == userId &&
                r.ReportID == reportId &&
                r.PaymentStatus == PaymentStatusEnum.Completed);
        }

        public async Task UpdateAsync(ReportOrder order)
        {
            _context.ReportOrders.Update(order);
            await _context.SaveChangesAsync();
        }

        // =========================
        // 🔥 Admin Analytics
        // =========================

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.ReportOrders
                .Where(r => r.PaymentStatus == PaymentStatusEnum.Completed)
                .SumAsync(r => r.Amount);
        }

        public async Task<decimal> GetRevenueTodayAsync()
        {
            var today = DateTime.UtcNow.Date;
            return await _context.ReportOrders
                .Where(r =>
                    r.PaymentStatus == PaymentStatusEnum.Completed &&
                    r.CreatedAt >= today)
                .SumAsync(r => r.Amount);
        }

        public async Task<decimal> GetRevenueThisWeekAsync()
        {
            var weekAgo = DateTime.UtcNow.AddDays(-7);
            return await _context.ReportOrders
                .Where(r =>
                    r.PaymentStatus == PaymentStatusEnum.Completed &&
                    r.CreatedAt >= weekAgo)
                .SumAsync(r => r.Amount);
        }

        public async Task<decimal> GetRevenueThisMonthAsync()
        {
            var startOfMonth = new DateTime(
                DateTime.UtcNow.Year,
                DateTime.UtcNow.Month,
                1);

            return await _context.ReportOrders
                .Where(r =>
                    r.PaymentStatus == PaymentStatusEnum.Completed &&
                    r.CreatedAt >= startOfMonth)
                .SumAsync(r => r.Amount);
        }

        public async Task<ReportOrder?> GetByOrderIdAsync(long orderId)
        {
            return await _context.ReportOrders.FirstOrDefaultAsync(p => p.Id == orderId);
        }

        public async Task<ReportOrder?> GetByReportIdAsync(int reportId)
        {
            return await _context.ReportOrders.FirstOrDefaultAsync(r => r.Id == reportId);
        }
    }

}
