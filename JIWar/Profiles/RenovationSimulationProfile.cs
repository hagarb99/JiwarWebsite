using AutoMapper;
using GEWAR.Models;
using Jiwar.DTOs;
using Jiwar.DTOs.RenovationDTOs;
using Jiwar.Models;
using Jiwar.Enum;


namespace Jiwar.Profiles
{
    public class RenovationSimulationProfile : Profile
    {
        public RenovationSimulationProfile()
        {
             //1-Simulation Details
            CreateMap<SimulationDetails, UpdateSimulationDetailsDto>()
                .ReverseMap();

            //2-Media
            CreateMap<SimulationMedia, UploadSimulationMediaDto>()
                .ReverseMap();

            //3-Recommendation (direct mapping)
            CreateMap<SimulationRecommendation, SimulationRecommendationDto>();

            //4-RenovationSimulation → Result DTO
            CreateMap<RenovationSimulation, SimulationResultDto>()
                .ForMember(dest => dest.SimulationId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Goals,
                    opt => opt.Ignore()) // ⚠️ handled in service
                .ForMember(dest => dest.Medias,
                    opt => opt.MapFrom(src => src.Medias))
                .ForMember(dest => dest.Recommendations,
                    opt => opt.MapFrom(src => src.Recommendations));

            //5-Analysis Result
            CreateMap<RenovationSimulation, SimulationAnalysisResultDto>()
                .ForMember(dest => dest.SimulationId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Condition,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Issues,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Recommendations,
                    opt => opt.MapFrom(src => src.Recommendations));




        }
    }
}
