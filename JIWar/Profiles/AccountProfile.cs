using AutoMapper;
using GEWAR.Models;
using Jiwar.Account;
using Jiwar.Account.DTOs;
using Jiwar.DTOs;
using Jiwar.DTOs.AccountDTOs.EditProfileDtos;
using Jiwar.Models;
using Microsoft.Extensions.Options;

namespace Jiwar.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<RegisterDto, User>().ForMember(destination => destination.RegistrationDate,
                      options => options.MapFrom(_ => DateTime.UtcNow));

            CreateMap<User, UserResponseDTO>();
            CreateMap<User, UserProfileDto>();
            CreateMap<PropertyOwner, PropertyOwnerDto>();
            CreateMap<InteriorDesigner, InteriorDesignerDto>();
            CreateMap<Property, PropertyDto>();


            CreateMap<EditProfileBaseDto, User>()
     .ForAllMembers(opt =>
         opt.Condition((src, dest, srcValue) => srcValue != null));

            CreateMap<PropertyOwnerEditProfileDto, PropertyOwner>()
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcValue) => srcValue != null));



        }

    }
}
