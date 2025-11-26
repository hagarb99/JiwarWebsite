using GEWAR;
using GEWAR.Models;
using Jiwar.Models;
using Microsoft.EntityFrameworkCore;

namespace Jiwar.Repositories
{
    public class ReportOrderRepository : GenericRepository<ReportOrder>, IReportOrderRepository
    {
        private readonly GiwarContext _context;

        public ReportOrderRepository(GiwarContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ReportOrder?> GetByReferenceAsync(string reference)
        {
            return await _context.ReportOrders.FirstOrDefaultAsync(r => r.PaymentReference == reference);
        }

        public async Task<bool> HasUserPaidForReport(string userId, int reportId)
        {
            return await _context.ReportOrders.AnyAsync(r =>
                r.UserID == userId &&
                r.ReportID == reportId &&
                r.PaymentStatus == PaymentStatusEnum.Completed);
        }

        public Task UpdateAsync(ReportOrder order)
        {
            throw new NotImplementedException();
        }
    }
}
