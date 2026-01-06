using AutoMapper;
using GEWAR.Models;
using Jiwar.DTOs;
using Jiwar.DTOs.AdminAnalytics;
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

            CreateMap<User, AdminUserDTO>()
                        .ForMember(dest => dest.Id,
                            opt => opt.MapFrom(src => src.Id))

                        .ForMember(dest => dest.UserName,
                            opt => opt.MapFrom(src => src.UserName))

                        .ForMember(dest => dest.Email,
                            opt => opt.MapFrom(src => src.Email))

                        .ForMember(dest => dest.Role,
                            opt => opt.MapFrom(src => src.Role))

                        .ForMember(dest => dest.RegistrationDate,
                            opt => opt.MapFrom(src => src.RegistrationDate))

                        .ForMember(dest => dest.IsActive,
                            opt => opt.MapFrom(src =>
                                src.LockoutEnd == null || src.LockoutEnd <= DateTimeOffset.UtcNow
                            ));
        }
    }
}
