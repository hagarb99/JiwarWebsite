namespace Jiwar.Repositories
{
    using GEWAR;
    using GEWAR.Models;
    using Microsoft.EntityFrameworkCore;
    
    public class BookingRepository : IBookingRepository
    {
        private readonly GiwarContext _context;

        public BookingRepository(GiwarContext context)
        {
            _context = context;
        }

        public async Task<Booking> AddAsync(Booking booking)
        {
            await _context.Booking.AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<bool> UpdateAsync(Booking booking)
        {
            _context.Booking.Update(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
                return false;

            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            return await _context.Booking
                .Include(b => b.Customer)
                .Include(b => b.Property)
                .Include(b => b.Offer)
                .Include(b => b.BookingRating)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _context.Booking
                .Include(b => b.Customer)
                .Include(b => b.Property)
                .ToListAsync();
        }
    }

}
