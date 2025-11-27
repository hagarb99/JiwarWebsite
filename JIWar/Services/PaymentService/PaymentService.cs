using GEWAR.Models;
using Jiwar.Models;
using Jiwar.Repositories;

namespace Jiwar.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IReportOrderRepository _orderRepo;
        private readonly IReportRepository _reportRepo;
        private readonly IBookingRepository _bookingRepo;
        private readonly IBookingPaymentRepository _bookingPaymentRepo;

        public PaymentService(IReportOrderRepository orderRepo
            , IReportRepository reportRepo
            , IBookingRepository bookingRepo
            , IBookingPaymentRepository bookingPaymentRepo)
        {
            _orderRepo = orderRepo;
            _reportRepo = reportRepo;
            _bookingRepo = bookingRepo;
            _bookingPaymentRepo = bookingPaymentRepo;
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
        public async Task<string> CreateBookingPaymentRequest(string userId, int bookingId, PaymentMethod method)
        {
            var booking = await _bookingRepo.GetByIdAsync(bookingId);
            if (booking == null)
                throw new Exception("Booking not found");

            var reference = Guid.NewGuid().ToString("N");

            var payment = new BookingPayment
            {
                UserID = userId,
                BookingID = bookingId,
                Amount = booking.Cost,
                PaymentMethod = method,
                PaymentStatus = PaymentStatusEnum.Pending,
                PaymentReference = reference,
                CreatedAt = DateTime.UtcNow
            };

            await _bookingPaymentRepo.AddAsync(payment);

            var fakePaymentUrl = $"https://payment.fakegateway.com/pay?ref={reference}";
            return fakePaymentUrl;
        }

        public async Task<bool> ConfirmBookingPayment(string paymentReference)
        {
            var payment = await _bookingPaymentRepo.GetByReferenceAsync(paymentReference);
            if (payment == null)
                return false;

            payment.PaymentStatus = PaymentStatusEnum.Completed;
            await _bookingPaymentRepo.UpdateAsync(payment);

            var booking = await _bookingRepo.GetByIdAsync(payment.BookingID);
            if (booking != null)
            {
                booking.PaymentStatus = PaymentStatusEnum.Completed;
                booking.status = StatusEnum.Confirmed;
                await _bookingRepo.UpdateAsync(booking);
            }

            return true;
        }

        public async Task<bool> HasUserPaidForBooking(string userId, int bookingId)
        {
            return await _bookingPaymentRepo.HasUserPaidForBooking(userId, bookingId);
        }

    }
}
