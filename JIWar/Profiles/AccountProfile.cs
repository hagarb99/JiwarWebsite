using AutoMapper;
using GEWAR.Models;
using Jiwar.Account;
using Jiwar.Account.DTOs;
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

            CreateMap<EditProfileDto, User>().ForAllMembers(op => op.Condition((source, destination, sourceValue) => sourceValue != null));

        }

    }
}
