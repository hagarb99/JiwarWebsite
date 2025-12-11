
using Jiwar.DTOs.ProposalDto;
using Jiwar.DTOs.UploadDesignDto;


using AutoMapper;
using GEWAR.Models; // أو namespace الموديلز عندك

public class DesignerProfile : Profile
{
    public DesignerProfile()
    {
        // مثال Mapping بين موديل وداتا ترانسفير أوبجكت (DTO)
        CreateMap<InteriorDesigner, DesignerProfileDto>();
        CreateMap<DesignerProfileDto, InteriorDesigner>();
    }
}

public class DesignerProfileDto
    {
        public string UserID { get; set; }
        public string Specialization { get; set; }
        public int? ExperienceYears { get; set; }
        public string PortfolioURL { get; set; }

        public List<UploadDesignDto> Designs { get; set; }
        public List<ProposalDto> Proposals { get; set; }
    }


