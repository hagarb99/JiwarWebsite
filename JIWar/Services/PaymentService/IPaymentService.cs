namespace Jiwar.Services
{
    public interface IPaymentService
    {
        //reports payment
        Task<string> CreatePaymentRequest(string userId, int reportId, PaymentMethod method);
        Task<bool> ConfirmPayment(string paymentReference);
        Task<bool> HasUserPaidForReport(string userId, int reportId);
        //booking payment
        Task<string> CreateBookingPaymentRequest(string userId, int bookingId, PaymentMethod method);
        Task<bool> ConfirmBookingPayment(string paymentReference);
        Task<bool> HasUserPaidForBooking(string userId, int bookingId);

    }
}
