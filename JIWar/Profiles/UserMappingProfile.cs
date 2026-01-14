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
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.statusEnum2.ToString()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Owneruser.Name))
    .ForMember(dest => dest.ProfilePicURL, opt => opt.MapFrom(src => src.Owneruser.ProfilePicURL))
    .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Owneruser.Bio));

            CreateMap<PropertyOwnerEditProfileDto, PropertyOwner>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Property, PropertyDto>();

            CreateMap<InteriorDesigner, InteriorDesignerDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.ProfilePicURL, opt => opt.MapFrom(src => src.User.ProfilePicURL))
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialization)) // ربط Specialty بـ Specialization
                .ForMember(dest => dest.ExperienceYears, opt => opt.MapFrom(src => src.ExperienceYears))
                .ForMember(dest => dest.PortfolioURL, opt => opt.MapFrom(src => src.PortfolioURL));

            CreateMap<InteriorDesignerEditProfileDto, InteriorDesigner>()
     // نأخذ من Website ونضع في PortfolioURL (الموجود في قاعدة البيانات)
     .ForMember(dest => dest.PortfolioURL, opt => opt.MapFrom(src => src.Website))

     // نأخذ أول عنصر من المصفوفة ونضعه في Specialization (لأن قاعدة البيانات string)
     .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src =>
         (src.Specializations != null && src.Specializations.Any()) ? src.Specializations[0] : null))

     .ForMember(dest => dest.ExperienceYears, opt => opt.MapFrom(src => src.YearsOfExperience))
     .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<User, AdminUserDTO>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src =>
                    src.LockoutEnd == null || src.LockoutEnd <= DateTimeOffset.UtcNow));

            CreateMap<User, UserResponseDTO>();
        }
    }
}
