using GEWAR;
using GEWAR.Models;
using Jiwar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Jiwar.Repositories
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllAsync();
        Task<Booking> GetByIdAsync(int id);
        Task<Booking> AddAsync(Booking booking);
        Task<bool> UpdateAsync(Booking booking);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Booking>> GetBookingsByCustomer(string customerId);
        Task<IEnumerable<Booking>> GetBookingsByProperty(int PropertyID);

        Task<Booking> GetBookingWithRating(int id);
        Task<IEnumerable<Booking>> GetBookingsForOwner(string ownerId);
        Task<bool> HasAnyPreviousBookingAsync(string customerId);



    }
}
