using Jiwar.DTOs.DesignDto;
using Jiwar.Models;
using AutoMapper;

namespace Jiwar.Profiles
{
    public class DesignerProposalProfile : Profile
    {
        public DesignerProposalProfile()
        {
            CreateMap<DesignerProposal, ProposalDto>();

            CreateMap<ProposalDto, DesignerProposal>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }

    }
}
