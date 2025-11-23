using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jiwar.Models;
using Jiwar.Repositories;
using Jiwar.DTOs.BookingDTOs;
using GEWAR.Models.Configurations;
using System.Collections.Generic;
using System.Threading.Tasks;
using GEWAR.Models;


namespace Jiwar.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DbContext _context;

        public BookingRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _context.Set<Booking>()
                .Include(b => b.Customer)
                .Include(b => b.Property)
                .Include(b => b.Offer)
                .Include(b => b.BookingRating)
                .ToListAsync();
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            return await _context.Set<Booking>()
                .Include(b => b.Customer)
                .Include(b => b.Property)
                .Include(b => b.Offer)
                .Include(b => b.BookingRating)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Booking> AddAsync(Booking booking)
        {
            await _context.Set<Booking>().AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<bool> UpdateAsync(Booking booking)
        {
            _context.Set<Booking>().Update(booking);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var booking = await _context.Set<Booking>().FindAsync(id);
            if (booking != null)
            {
                _context.Set<Booking>().Remove(booking);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Booking>> GetBookingsByCustomer(string customerId)
        {
            return await _context.Set<Booking>()
                .Where(b => b.CustomerID == customerId)
                .Include(b => b.Property)
                .Include(b => b.Offer)
                .ToListAsync();
        }

        public async Task<Booking> GetBookingWithRating(int id)
        {
            return await _context.Set<Booking>()
                .Include(b => b.BookingRating)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
