using Jiwar.DTOs.DesignDto;
using Jiwar.Models;
using AutoMapper;

namespace Jiwar.Profiles
{
    public class DesignerProposalProfile : Profile
    {
        public DesignerProposalProfile()
        {
            CreateMap<DesignerProposal, ProposalDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.StatusEnumReq.ToString()))
                .ForMember(dest => dest.PriceEstimate, opt => opt.MapFrom(src => src.EstimatedCost))
                . ForMember(dest => dest.OfferDetails, opt => opt.MapFrom(src => src.ProposalDescription))
                .ForMember(dest => dest.RequestID, opt => opt.MapFrom(src => src.DesignRequestID));

            CreateMap<ProposalDto, DesignerProposal>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DesignRequestID, opt => opt.MapFrom(src => src.RequestID))
                .ForMember(dest => dest.EstimatedCost, opt => opt.MapFrom(src => src.PriceEstimate))
                .ForMember(dest => dest.ProposalDescription, opt => opt.MapFrom(src => src.OfferDetails));
        }

    }
}
