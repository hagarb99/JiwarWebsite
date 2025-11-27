using Jiwar.Models;

namespace Jiwar.Repositories
{
    public interface IBookingPaymentRepository : IGenericRepository<BookingPayment>
    {
        Task<BookingPayment?> GetByReferenceAsync(string reference);
        Task<bool> HasUserPaidForBooking(string userId, int bookingId);
        Task UpdateAsync(BookingPayment payment);

    }
}
