using System.Collections.Generic;
using System.Threading.Tasks;
using GEWAR;
using Jiwar.Models;
using JIWAR.Models;
using Microsoft.EntityFrameworkCore;
namespace Jiwar.Repositories
{

    namespace Jiwar.Services
    {
        public class BookingRatingService
        {
            private readonly GiwarContext _context;

            public BookingRatingService(GiwarContext context)
            {
                _context = context;
            }

            // Add
            public async Task<BookingRating> AddBookingRatingAsync(BookingRating br)
            {
                _context.BookingRating.Add(br);
                await _context.SaveChangesAsync();
                return br;
            }

            // Get all
            public async Task<List<BookingRating>> GetAllBookingRatingsAsync()
            {
                return await _context.BookingRating
                                     .Include(br => br.Booking)
                                     .Include(br => br.User)
                                     .ToListAsync();
            }

            // Update
            public async Task<BookingRating> UpdateBookingRatingAsync(BookingRating br)
            {
                _context.BookingRating.Update(br);
                await _context.SaveChangesAsync();
                return br;
            }

            // Delete
            public async Task<bool> DeleteBookingRatingAsync(int id)
            {
                var br = await _context.BookingRating.FindAsync(id);
                if (br == null) return false;

                _context.BookingRating.Remove(br);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }

}
