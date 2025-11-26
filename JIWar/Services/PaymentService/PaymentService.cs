using GEWAR.Models;
using Jiwar.Models;
using Jiwar.Repositories;

namespace Jiwar.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IReportOrderRepository _orderRepo;
        private readonly IReportRepository _reportRepo;

        public PaymentService(IReportOrderRepository orderRepo, IReportRepository reportRepo)
        {
            _orderRepo = orderRepo;
            _reportRepo = reportRepo;
        }

        public async Task<string> CreatePaymentRequest(string userId, int reportId, PaymentMethod method)
        {
            var report = await _reportRepo.GetByIdAsync(reportId);
            if (report == null)
                throw new Exception("Report not found");

            var reference = Guid.NewGuid().ToString("N");

            var order = new ReportOrder
            {
                UserID = userId,
                ReportID = reportId,
                Amount = method == PaymentMethod.Paymob ? 199 : 299, 
                PaymentMethod = method,
                PaymentStatus = PaymentStatusEnum.Pending,
                PaymentReference = reference,
                CreatedAt = DateTime.UtcNow
            };
            await _orderRepo.AddAsync(order);
            // Simulate payment gateway URL generation
            var fakePaymentUrl = $"https://payment.fakegateway.com/pay?ref={reference}";
            return fakePaymentUrl;
        }

        public async Task<bool> ConfirmPayment(string paymentReference)
        {
            var order = await _orderRepo.GetByReferenceAsync(paymentReference);
            if (order == null)
                return false;

            order.PaymentStatus = PaymentStatusEnum.Completed;
            await _orderRepo.UpdateAsync(order);
            return true;
        }

        public async Task<bool> HasUserPaidForReport(string userId, int reportId)
        {
            return await _orderRepo.HasUserPaidForReport(userId, reportId);
        }

    }
}
