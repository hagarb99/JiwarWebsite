using Jiwar.DTOs.BookingDTOs;

namespace Jiwar.Repositories
{
    public interface IBookingService
    {
        Task<BookingDto> GetByIdAsync(int id);
        Task<List<BookingDto>> GetAllAsync();
        Task<BookingDto> CreateAsync(CreateBookingDto dto);
        Task<bool> UpdateAsync(int id, CreateBookingDto dto);
        Task<bool> DeleteAsync(int id);
    }

}
