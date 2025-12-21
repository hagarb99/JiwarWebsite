using AutoMapper;
using Jiwar.DTOs.DesignDto;
using Jiwar.Models;

namespace Jiwar.Profiles
{
    public class DesignRequestProfile : Profile
    {
        public DesignRequestProfile()
        {
            CreateMap<DesignRequest, DesignRequestDto>()
                .ForMember(dest => dest.ProposalCount,
                           opt => opt.MapFrom(src => src.Proposals != null ? src.Proposals.Count : 0));

            CreateMap<DesignRequestDto, DesignRequest>()
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Proposals, opt => opt.Ignore());
        }
    }
}
