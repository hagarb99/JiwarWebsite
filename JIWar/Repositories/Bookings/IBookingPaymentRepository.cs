using Jiwar.Models;
using Jiwar.Repositories;

namespace Jiwar.Repositories
{
    public interface IBookingPaymentRepository : IGenericRepository<BookingPayment>
    {
        Task<BookingPayment?> GetByBookingIdAsync(int bookingId);
        Task<BookingPayment?> GetByReferenceAsync(string reference);
        Task<bool> HasUserPaidForBooking(string userId, int bookingId);
        Task UpdateAsync(BookingPayment payment);
        Task<BookingPayment?> GetByOrderIdAsync(long orderId);
public interface IBookingPaymentRepository : IGenericRepository<BookingPayment>
{
    Task<BookingPayment?> GetByReferenceAsync(string reference);
    Task<bool> HasUserPaidForBooking(string userId, int bookingId);
    Task UpdateAsync(BookingPayment payment);

    // 🔹 Analytics Methods
    Task<decimal> GetTotalRevenueAsync();
    Task<decimal> GetTodayRevenueAsync();
    Task<decimal> GetWeekRevenueAsync();
    Task<decimal> GetMonthRevenueAsync();
}
