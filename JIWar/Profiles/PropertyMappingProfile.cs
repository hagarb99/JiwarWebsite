using AutoMapper;
using GEWAR.Models;
using Jiwar.DTOs.PropertyDTOs;
using JIWar.PropertyOwner;

namespace Jiwar.Profiles
{
    public class PropertyMappingProfile : Profile
    {
        public PropertyMappingProfile()
        {
            // Property → PropertyListBDTO (للـ GetAll و Browse)
            CreateMap<Property, PropertyListBDTO>()
                .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src =>
                    src.PropertyMedia.OrderBy(m => m.Id).Select(m => m.MediaURL).FirstOrDefault()));

            // Property → PropertyDetailsDTO (للـ Details و MyProperties)
            CreateMap<Property, PropertyDetailsDTO>()
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src =>
                    src.PropertyOwner != null && src.PropertyOwner.Owneruser != null
                    ? src.PropertyOwner.Owneruser.UserName
                    : "غير معروف"))
                .ForMember(dest => dest.MediaUrls, opt => opt.MapFrom(src =>
                    src.PropertyMedia.Where(m => !m.IsDeleted).Select(m => m.MediaURL).ToList()))
                .ForMember(dest => dest.PublishedAt, opt => opt.MapFrom(src => src.CreatedDate));
        }
    }
}
