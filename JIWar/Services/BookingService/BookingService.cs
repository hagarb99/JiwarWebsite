using AutoMapper;
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
        private readonly IPropertyRepository _propertyRepo;
        private readonly IMapper mapper;
        public BookingService(
            IBookingRepository bookingRepo , 
            IPropertyRepository _propertyRepo,
            IMapper mapper)
        {
            _bookingRepo = bookingRepo;
            this._propertyRepo = _propertyRepo;
            this.mapper = mapper;
        }

        public async Task<BookingDto> GetByIdAsync(int id)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null) return null;

            return mapper.Map<BookingDto>(booking);
        }

        public async Task<List<BookingDto>> GetAllAsync()
        {
            var bookings = await _bookingRepo.GetAllAsync();
            return mapper.Map<List<BookingDto>>(bookings);
        }

        public async Task<BookingDto> CreateAsync(CreateBookingDto dto, string customerId)
        {
            // 1. Get property details
            var property = await _propertyRepo.GetPropertyDetailsAsync(dto.PropertyID);

            if (property == null)
                throw new Exception("Property not found");

            // 2. Validate dates
            if (dto.StartDate >= dto.EndDate)
                throw new Exception("End date must be after start date");

            // 3. Check for overlapping bookings
            var overlapping = (await _bookingRepo.GetBookingsByProperty(dto.PropertyID))
                .Any(b => b.StartDate < dto.EndDate && dto.StartDate < b.EndDate);
            if (overlapping)
                throw new Exception("Property already booked for selected dates");

            // 4. Calculate cost
            var totalDays = (dto.EndDate - dto.StartDate).Days;
            var cost = property.Price * totalDays;

            // TODO: Apply offer if dto.OfferID is provided
            //int? offerId = dto.OfferID == 0 ? null : dto.OfferID;

            
            var booking = mapper.Map<Booking>(dto);
            booking.CustomerID = customerId;
            booking.Cost = cost;
            booking.status = StatusEnum.Pending;
            booking.PaymentStatus = PaymentStatusEnum.Pending;
            booking.PaymentMethod = PaymentMethod.Paymob;

            var created = await _bookingRepo.AddAsync(booking);
            return mapper.Map<BookingDto>(created);
        }


        public async Task<bool> UpdateAsync(int id, CreateBookingDto dto)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);
            if (booking == null) return false;

            mapper.Map(dto, booking);

            return await _bookingRepo.UpdateAsync(booking);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _bookingRepo.DeleteAsync(id);
        }

    }
}