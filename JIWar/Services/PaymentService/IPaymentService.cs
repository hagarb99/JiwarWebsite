namespace Jiwar.Services
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentRequest(string userId, int reportId, PaymentMethod method);
        Task<bool> ConfirmPayment(string paymentReference);
        Task<bool> HasUserPaidForReport(string userId, int reportId);

    }
}
