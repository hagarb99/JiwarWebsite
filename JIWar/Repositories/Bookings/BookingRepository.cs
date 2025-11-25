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
using GEWAR;


namespace Jiwar.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly GiwarContext giwarContext;

        public BookingRepository(GiwarContext giwarContext)
        {
            this.giwarContext = giwarContext;
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await giwarContext.Set<Booking>()
                .Include(b => b.Customer)
                .Include(b => b.Property)
                .Include(b => b.Offer)
                .Include(b => b.BookingRating)
                .ToListAsync();
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            return await giwarContext.Set<Booking>()
                .Include(b => b.Customer)
                .Include(b => b.Property)
                .Include(b => b.Offer)
                .Include(b => b.BookingRating)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Booking> AddAsync(Booking booking)
        {
            await giwarContext.Set<Booking>().AddAsync(booking);
            await giwarContext.SaveChangesAsync();
            return booking;
        }

        public async Task<bool> UpdateAsync(Booking booking)
        {
            giwarContext.Set<Booking>().Update(booking);
            return await giwarContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var booking = await giwarContext.Set<Booking>().FindAsync(id);
            if (booking != null)
            {
                giwarContext.Set<Booking>().Remove(booking);
                 await giwarContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Booking>> GetBookingsByCustomer(string customerId)
        {
            return await giwarContext.Set<Booking>()
                .Where(b => b.CustomerID == customerId)
                .Include(b => b.Property)
                .Include(b => b.Offer)
                .ToListAsync();
        }

        public async Task<Booking> GetBookingWithRating(int id)
        {
            return await giwarContext.Set<Booking>()
                .Include(b => b.BookingRating)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
