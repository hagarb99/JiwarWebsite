using GEWAR.Models;
using Jiwar.DTOs;
using Jiwar.DTOs.BookingDTOs;

namespace Jiwar.Repositories
{
    public interface IBookingService
    {
        Task<BookingDto> GetByIdAsync(int id);
        Task<List<BookingDto>> GetAllAsync();
        Task<BookingDto> CreateAsync(CreateBookingDto dto, string customerId);
        Task<bool> UpdateAsync(int id, CreateBookingDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<BookingDto>> GetBookingsByCustomerAsync(string customerId);
        Task<List<OwnerBookingDto>> GetBookingsForOwnerAsync(string ownerId);
        Task<bool> UpdateBookingStatusAsync(int bookingId, StatusEnum status, string ownerId);

    }

}
