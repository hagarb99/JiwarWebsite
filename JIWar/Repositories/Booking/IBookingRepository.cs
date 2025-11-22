using Jiwar.Models.Booking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jiwar.Repositories.Booking
{
	public interface IBookingRepository
	{
		Task<List<Booking>> GetAllAsync();
		Task<Booking> GetByIdAsync(int id);
		Task<Booking> AddAsync(Booking booking);
		Task<bool> UpdateAsync(Booking booking);
		Task<bool> DeleteAsync(int id);

		Task<IEnumerable<Booking>> GetBookingsByCustomer(string customerId);
		Task<Booking> GetBookingWithRating(int id);
	}
}
