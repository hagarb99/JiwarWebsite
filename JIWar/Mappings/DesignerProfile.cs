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
            CreateMap<Request, RequestDto>().ReverseMap();
            CreateMap<Proposal, ProposalDto>().ReverseMap();

            // DesignRequest
            CreateMap<DesignRequest, DesignRequestDto>()
                .ForMember(dest => dest.ProposalCount,
                           opt => opt.MapFrom(src => src.Proposals != null ? src.Proposals.Count : 0));

            CreateMap<DesignRequestDto, DesignRequest>();

            // DesignerProposal
            CreateMap<DesignerProposal, DesignerProposalDto>().ReverseMap();

            // Final Design
            CreateMap<Design, DesignDto>().ReverseMap();
            CreateMap<CreateDesignDto, Design>();
        }
    }
}