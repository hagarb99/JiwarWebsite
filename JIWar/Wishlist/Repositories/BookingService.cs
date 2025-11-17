using GEWAR.Models;
using Jiwar.DTOs;

namespace Jiwar.Repositories
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repo;

        public BookingService(IBookingRepository repo)
        {
            _repo = repo;
        }

        public async Task<BookingDto> GetByIdAsync(int id)
        {
            var booking = await _repo.GetByIdAsync(id);
            if (booking == null) return null;

            return new BookingDto
            {
                Id = booking.Id,
                PropertyID = booking.PropertyID,
                CustomerID = booking.CustomerID,
                OfferID = booking.OfferID,
                Status = booking.status.ToString(),
                PaymentStatus = booking.PaymentStatus?.ToString(),
                Cost = booking.Cost,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate
            };
        }

        public async Task<List<BookingDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(b => new BookingDto
            {
                Id = b.Id,
                PropertyID = b.PropertyID,
                CustomerID = b.CustomerID,
                OfferID = b.OfferID,
                Status = b.status.ToString(),
                PaymentStatus = b.PaymentStatus?.ToString(),
                Cost = b.Cost,
                StartDate = b.StartDate,
                EndDate = b.EndDate
            }).ToList();
        }

        public async Task<BookingDto> CreateAsync(CreateBookingDto dto)
        {
            Booking booking = new Booking
            {
                PropertyID = dto.PropertyID,
                CustomerID = dto.CustomerID,
                OfferID = dto.OfferID,
                status = dto.Status,
                PaymentStatus = dto.PaymentStatus,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Cost = dto.Cost
            };

            booking = await _repo.AddAsync(booking);

            return await GetByIdAsync(booking.Id);
        }

        public async Task<bool> UpdateAsync(int id, CreateBookingDto dto)
        {
            var booking = await _repo.GetByIdAsync(id);
            if (booking == null) return false;

            booking.PropertyID = dto.PropertyID;
            booking.CustomerID = dto.CustomerID;
            booking.OfferID = dto.OfferID;
            booking.status = dto.Status;
            booking.PaymentStatus = dto.PaymentStatus;
            booking.StartDate = dto.StartDate;
            booking.EndDate = dto.EndDate;
            booking.Cost = dto.Cost;

            return await _repo.UpdateAsync(booking);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }

}
