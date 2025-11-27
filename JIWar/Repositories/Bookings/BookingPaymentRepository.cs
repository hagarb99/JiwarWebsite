using GEWAR;
using GEWAR.Models;
using Jiwar.Models;
using Microsoft.EntityFrameworkCore;

namespace Jiwar.Repositories
{
    public class BookingPaymentRepository : GenericRepository<BookingPayment>, IBookingPaymentRepository
    {
        private readonly GiwarContext _context;

        public BookingPaymentRepository(GiwarContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BookingPayment?> GetByReferenceAsync(string reference)
        {
            return await _context.BookingPayments.FirstOrDefaultAsync(p => p.PaymentReference == reference);
        }

        public async Task<bool> HasUserPaidForBooking(string userId, int bookingId)
        {
            return await _context.BookingPayments.AnyAsync(p =>
                p.UserID == userId &&
                p.BookingID == bookingId &&
                p.PaymentStatus == PaymentStatusEnum.Completed);
        }

        public async Task UpdateAsync(BookingPayment payment)
        {
            _context.BookingPayments.Update(payment);
            await _context.SaveChangesAsync();
        }

    }
}
