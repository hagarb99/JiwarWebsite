using AutoMapper;
using Jiwar.DTOs;
using Jiwar.Models;
using JIWar.PropertyOwner;

namespace Jiwar.Mappings
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<PropertyDto, PropertyDetailsDTO>();
        }
    }
}
