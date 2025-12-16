using Jiwar.DTOs;

namespace Jiwar.Services
{
    public interface IPaymentService
    {
        Task<string> CreateBookingPaymentAsync(string userId, int bookingId);
        Task<string> CreateReportPaymentAsync(string userId, int reportId);
        Task HandlePaymobWebhookAsync(PaymobWebhookDto dto);
        Task<bool> HasUserPaidForBookingAsync(string userId, int bookingId);
        Task<bool> HasUserPaidForReportAsync(string userId, int reportId);

    }
}
