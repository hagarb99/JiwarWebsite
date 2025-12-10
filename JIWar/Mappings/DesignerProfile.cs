using AutoMapper;
using GEWAR.Models;
using Jiwar.DTOs.CreateProposalDto;
using Jiwar.DTOs.DesignDto;
using Jiwar.DTOs.ProposalDto;
using Jiwar.DTOs.RequestDto;
using Jiwar.DTOs.UploadDesignDto;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Request
        CreateMap<Request, RequestDto>().ReverseMap();

        // Proposal
        CreateMap<Proposal, ProposalDto>().ReverseMap();
        CreateMap<CreateProposalDto, Proposal>();

        // Design
        CreateMap<Design, DesignDto>().ReverseMap();
        CreateMap<UploadDesignDto, Design>();

        // Designer Profile
        CreateMap<InteriorDesigner, DesignerProfileDto>();
    }
}

