using AutoMapper;
using GEWAR.Models;
using Jiwar.DTOs.BookingDTOs;
using Jiwar.Models;

namespace Jiwar.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.PaymentStatus,
                           opt => opt.MapFrom(src => src.PaymentStatus ?? PaymentStatusEnum.Pending));

            CreateMap<CreateBookingDto, Booking>()
                .ForMember(dest => dest.OfferID,
                           opt => opt.Condition(src => src.OfferID != 0));
        }
    }
}
