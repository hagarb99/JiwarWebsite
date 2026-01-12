using AutoMapper;
using GEWAR;
using GEWAR.Models;
using Jiwar.DTOs;
using Jiwar.DTOs.BookingDTOs;
using Jiwar.Enum;
using Jiwar.Hubs;
using Jiwar.Models;
using Jiwar.Repositories;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jiwar.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly IPropertyRepository _propertyRepo;
        private readonly IHubContext<NotificationHub> _hubContext; 
        private readonly IMapper mapper;
        private readonly GiwarContext _context;
        public BookingService(
            IBookingRepository bookingRepo ,  
            IPropertyRepository _propertyRepo,
            IMapper mapper,
            IHubContext<NotificationHub> hubContext,
            GiwarContext context
            )
        {
            _bookingRepo = bookingRepo;
            this._propertyRepo = _propertyRepo;
            this.mapper = mapper;
            _hubContext = hubContext;
            _context = context;
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
                 .Any(b =>
        b.status == StatusEnum.Confirmed &&
        b.StartDate < dto.EndDate &&
        dto.StartDate < b.EndDate
    );
            if (overlapping)
                throw new Exception("Property already booked for selected dates");

            // 4. Calculate cost
            //var totalDays = (dto.EndDate - dto.StartDate).Days;
            //var cost = property.Price * totalDays;

            // TODO: Apply offer if dto.OfferID is provided
            //int? offerId = dto.OfferID == 0 ? null : dto.OfferID;

            
            var booking = mapper.Map<Booking>(dto);
            booking.CustomerID = customerId;
            //booking.Cost = cost;
            booking.status = StatusEnum.Pending;
            booking.PaymentStatus = PaymentStatusEnum.Pending;
            booking.PaymentMethod = PaymentMethod.Paymob;

            var created = await _bookingRepo.AddAsync(booking);
            

            if (string.IsNullOrEmpty(property.OwnerID))
                throw new Exception("Property owner ID is missing.");

            // Create & persist Notification so it appears in user's notification list
            var notification = new Notification
            {
                UserID = property.OwnerID,
                Title = "New Booking Request",
                Message = $"You have a new booking request for {booking.Property?.Title ?? "your property"}",
                NotificationType = NotificationType.Booking,
                SentDate = DateTime.UtcNow,
                IsRead = false,
                RelatedId = booking.Id.ToString()
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            // Send real-time notification using the same event & args as proposals
            await _hubContext.Clients.User(property.OwnerID)
                .SendAsync("ReceiveNotification", notification.Title, notification.Message);

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
        public async Task<List<CustomerBookingDto>> GetBookingsByCustomerAsync(string customerId)
        {
            var bookings = await _bookingRepo.GetBookingsByCustomer(customerId);

            return bookings.Select(b => new CustomerBookingDto
            {
                Id = b.Id,
                PropertyID = b.PropertyID,
                PropertyTitle = b.Property.Title,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                Status = b.status
            }).ToList();
        }
        public async Task<List<OwnerBookingDto>> GetBookingsForOwnerAsync(string ownerId)
        {
            var bookings = await _bookingRepo.GetBookingsForOwner(ownerId);

            return bookings.Select(b => new OwnerBookingDto
            {
                Id = b.Id,
                PropertyID = b.PropertyID,
                CustomerName = b.Customer != null ? b.Customer.Name : "Unknown",
                PropertyTitle = b.Property != null ? b.Property.Title : "Unknown",
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                Cost = b.Cost,
                Status = b.status
            }).ToList();
        }
        public async Task<bool> UpdateBookingStatusAsync(int bookingId, StatusEnum status, string ownerId)
        {
            var booking = await _bookingRepo.GetByIdAsync(bookingId);
            if (booking == null || booking.Property.OwnerID != ownerId) return false;

            booking.status = status;
            var updated = await _bookingRepo.UpdateAsync(booking);

            if (updated)
            {
                var notifTitle = status == StatusEnum.Confirmed ? "Booking Accepted" : "Booking Rejected";
                var notifMessage = $"Your booking for {booking.Property?.Title ?? "the property"} was {status.ToString().ToLower()}";

                var notification = new Notification
                {
                    UserID = booking.CustomerID,
                    Title = notifTitle,
                    Message = notifMessage,
                    NotificationType = NotificationType.Booking,
                    SentDate = DateTime.UtcNow,
                    IsRead = false,
                    RelatedId = booking.Id.ToString()
                };

                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();

                // Send real-time notification using same event as proposals
                await _hubContext.Clients.User(booking.CustomerID)
                    .SendAsync("ReceiveNotification", notification.Title, notification.Message);
            }

            return updated;
        }



    }
}