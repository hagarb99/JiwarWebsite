using AutoMapper;
using GEWAR.Models;
using Jiwar.DTOs;
using Jiwar.Models;

namespace Jiwar.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // User -> UserProfileDto
            CreateMap<User, UserProfileDto>()
                .ForMember(dest => dest.PropertyOwner, opt => opt.MapFrom(src => src.propertyOwner))
                .ForMember(dest => dest.InteriorDesigner, opt => opt.MapFrom(src => src.InteriorDesigner));

            // PropertyOwner -> PropertyOwnerDto
            CreateMap<PropertyOwner, PropertyOwnerDto>()
                .ForMember(dest => dest.PlanType, opt => opt.MapFrom(src => src.planTypeEnum.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.statusEnum2.ToString()));

            // Property -> PropertyDto
            CreateMap<Property, PropertyDto>();

            // InteriorDesigner -> InteriorDesignerDto
            CreateMap<InteriorDesigner, InteriorDesignerDto>();
        }
    }
}
