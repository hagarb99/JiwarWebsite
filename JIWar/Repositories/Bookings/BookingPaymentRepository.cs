using GEWAR;
using GEWAR.Models;
using Jiwar.Models;
using Jiwar.Repositories;
using Microsoft.EntityFrameworkCore;

public class BookingPaymentRepository
    : GenericRepository<BookingPayment>, IBookingPaymentRepository
{
    private readonly GiwarContext _context;

    public BookingPaymentRepository(GiwarContext context) : base(context)
    {
        _context = context;
    }

    public async Task<BookingPayment?> GetByReferenceAsync(string reference)
    {
        return await _context.BookingPayments
            .FirstOrDefaultAsync(p => p.PaymentReference == reference);
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

    // ===============================
    // 🔹 Analytics Methods
    // ===============================

    public async Task<decimal> GetTotalRevenueAsync()
    {
        return await _context.BookingPayments
            .Where(p => p.PaymentStatus == PaymentStatusEnum.Completed)
            .SumAsync(p => p.Amount);
    }

    public async Task<decimal> GetTodayRevenueAsync()
    {
        var today = DateTime.UtcNow.Date;

        return await _context.BookingPayments
            .Where(p =>
                p.PaymentStatus == PaymentStatusEnum.Completed &&
                p.CreatedAt.Date == today)
            .SumAsync(p => p.Amount);
    }

    public async Task<decimal> GetWeekRevenueAsync()
    {
        var lastWeek = DateTime.UtcNow.AddDays(-7);

        return await _context.BookingPayments
            .Where(p =>
                p.PaymentStatus == PaymentStatusEnum.Completed &&
                p.CreatedAt>= lastWeek)
            .SumAsync(p => p.Amount);
    }

    public async Task<decimal> GetMonthRevenueAsync()
    {
        var startOfMonth = new DateTime(
            DateTime.UtcNow.Year,
            DateTime.UtcNow.Month,
            1);

        return await _context.BookingPayments
            .Where(p =>
                p.PaymentStatus == PaymentStatusEnum.Completed &&
                p.CreatedAt >= startOfMonth)
            .SumAsync(p => p.Amount);
    }
}
