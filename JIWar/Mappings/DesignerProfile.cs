using AutoMapper;
using GEWAR.Models;
using Jiwar.DTOs.DesignDto;
using Jiwar.DTOs.ProposalDto;
using Jiwar.DTOs.RequestDto;
using Jiwar.Models;
namespace Jiwar.Mappings
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Requests (لو مستخدمين)
            CreateMap<DesignRequest, RequestDto>().ReverseMap();
            CreateMap<DesignerProposal, DTOs.DesignDto.ProposalDto>().ReverseMap();

            // DesignRequest
            CreateMap<DesignRequest, DesignRequestDto>()
                .ForMember(dest => dest.ProposalCount,
                           opt => opt.MapFrom(src => src.Proposals != null ? src.Proposals.Count : 0));

            CreateMap<DesignRequestDto, DesignRequest>();

            CreateMap<DesignerProposal, DTOs.DesignDto.ProposalDto>()
     .ForMember(dest => dest.DesignerName, opt => opt.MapFrom(src => src.Designer.User.Name))
     .ForMember(dest => dest.DesignerEmail, opt => opt.MapFrom(src => src.Designer.User.Email))
     .ReverseMap();


            // Final Design
            CreateMap<Design, DesignDto>().ReverseMap();
            CreateMap<CreateDesignDto, Design>();
        }
    }
}