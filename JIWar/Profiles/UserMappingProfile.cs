using AutoMapper;
using GEWAR.Models;
using Jiwar.Account;
using Jiwar.Account.DTOs;
using Jiwar.DTOs;
using Jiwar.DTOs.AccountDTOs.EditProfileDtos;
using Jiwar.DTOs.AdminAnalytics;
using Jiwar.Models;

namespace Jiwar.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<RegisterDto, User>();

            CreateMap<EditProfileBaseDto, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<User, UserProfileDto>()
                .ForMember(dest => dest.PropertyOwner, opt => opt.MapFrom(src => src.propertyOwner))
                .ForMember(dest => dest.InteriorDesigner, opt => opt.MapFrom(src => src.InteriorDesigner));

            CreateMap<PropertyOwner, PropertyOwnerDto>()
                .ForMember(dest => dest.PlanType, opt => opt.MapFrom(src => src.planTypeEnum.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.statusEnum2.ToString()));

            CreateMap<PropertyOwnerEditProfileDto, PropertyOwner>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Property, PropertyDto>();
            CreateMap<InteriorDesigner, InteriorDesignerDto>();

            CreateMap<User, AdminUserDTO>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src =>
                    src.LockoutEnd == null || src.LockoutEnd <= DateTimeOffset.UtcNow));

            CreateMap<User, UserResponseDTO>();
        }
    }
}
