using AutoMapper;
using GEWAR.Models;
using Jiwar.DTOs.DesignDto;
using Jiwar.DTOs.ProposalDto;
using Jiwar.DTOs.RequestDto;
using Jiwar.DTOs;
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
     .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.StatusEnumReq))
     .ForMember(dest => dest.DesignerName, opt => opt.MapFrom(src => src.Designer != null && src.Designer.User != null ? src.Designer.User.Name : ""))
     .ForMember(dest => dest.DesignerEmail, opt => opt.MapFrom(src => src.Designer != null && src.Designer.User != null ? src.Designer.User.Email : ""))
     .ReverseMap();


            // Profile Mappings
            CreateMap<User, UserProfileDto>();
            CreateMap<InteriorDesigner, InteriorDesignerDto>()
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialization))
                .ForMember(dest => dest.ProfilePicURL, opt => opt.MapFrom(src => src.PortfolioURL))
                //.ForMember(dest => dest.YearsOfExperience, opt => opt.MapFrom(src => src.ExperienceYears))
                .ForMember(dest => dest.Specializations, opt => opt.MapFrom(src => 
                    string.IsNullOrEmpty(src.Specializations) ? new List<string>() : src.Specializations.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()))
                .ForMember(dest => dest.Certifications, opt => opt.MapFrom(src => 
                    string.IsNullOrEmpty(src.Certifications) ? new List<string>() : src.Certifications.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()));

            // Final Design
            CreateMap<Design, DesignDto>().ReverseMap();
            CreateMap<CreateDesignDto, Design>();
        }
    }
}