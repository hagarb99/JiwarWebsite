using GEWAR.Models;
using Jiwar.DTOs.BookingDTOs;
using Jiwar.Models;
using Jiwar.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jiwar.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepo;

        public BookingService(IBookingRepository bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        public async Task<BookingDto> GetByIdAsync(int id)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null) return null;

            return MapToDto(booking);
        }

        public async Task<List<BookingDto>> GetAllAsync()
        {
            var bookings = await _bookingRepo.GetAllAsync();
            return bookings.Select(MapToDto).ToList();
        }

        public async Task<BookingDto> CreateAsync(CreateBookingDto dto)
        {
            var booking = new Booking
            {
                PropertyID = dto.PropertyID,
                CustomerID = dto.CustomerID,
                OfferID = dto.OfferID,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Cost = dto.Cost,
                status = StatusEnum.Pending,
                PaymentStatus = PaymentStatusEnum.Pending,
                PaymentMethod = dto.PaymentMethod
            };

            var created = await _bookingRepo.AddAsync(booking);
            return MapToDto(created);
        }

        public async Task<bool> UpdateAsync(int id, CreateBookingDto dto)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null) return false;

            booking.PropertyID = dto.PropertyID;
            booking.CustomerID = dto.CustomerID;
            booking.OfferID = dto.OfferID;
            booking.StartDate = dto.StartDate;
            booking.EndDate = dto.EndDate;
            booking.Cost = dto.Cost;
            booking.PaymentMethod = dto.PaymentMethod;

            return await _bookingRepo.UpdateAsync(booking);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _bookingRepo.DeleteAsync(id);
        }

        private BookingDto MapToDto(Booking booking)
        {
            return new BookingDto
            {
                Id = booking.Id,
                PropertyID = booking.PropertyID,
                CustomerID = booking.CustomerID,
                OfferID = booking.OfferID,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Cost = booking.Cost,
                PaymentStatus = booking.PaymentStatus ?? PaymentStatusEnum.Pending,
                PaymentMethod = booking.PaymentMethod


            };
        }
    }
}