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


            CreateMap<EditProfileDto, User>().ForAllMembers(op => op.Condition((source, destination, sourceValue) => sourceValue != null));
            CreateMap<EditProfileBaseDto, User>()
    .ForAllMembers(options =>
        options.Condition((source, destination, sourceValue) => sourceValue != null));

            CreateMap<PropertyOwnerEditProfileDto, PropertyOwner>()
    .ForAllMembers(opt =>
        opt.Condition((src, dest, srcValue) => srcValue != null));


        }

    }
}
