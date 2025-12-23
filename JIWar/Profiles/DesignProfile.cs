using AutoMapper;
using Jiwar.DTOs.DesignDto;
using Jiwar.Models;

namespace Jiwar.Profiles
{
    public class DesignProfile : Profile
    {
        public DesignProfile()
        {
            CreateMap<Design, DesignDto>();

            CreateMap<CreateDesignDto, Design>()
                .ForMember(dest => dest.CreationDate, opt => opt.Ignore())
                .ForMember(dest => dest.DesignerID, opt => opt.Ignore())
                .ForMember(dest => dest.OwnerID, opt => opt.Ignore());    
        }
    }
}
