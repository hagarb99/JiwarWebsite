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
            // mappings here 👇
            //CreateMap<SimulationDetails, SimulationDetailsDto>()
            //.ReverseMap();

            //CreateMap<SimulationMedia, UploadSimulationMediaDto>();

            //CreateMap<SimulationRecommendation, SimulationRecommendationDto>()
            //    .ForMember(dest => dest.IsAIGenerated,
            //        opt => opt.MapFrom(src => src.Source == RecommendationSourceEnum.AI));

            //CreateMap<RenovationSimulation, SimulationResultDto>()
            //    .ForMember(dest => dest.SimulationId,
            //        opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Goals,
            //        opt => opt.MapFrom(src =>
            //            string.IsNullOrWhiteSpace(src.RenovationGoalsJson)
            //                ? new List<string>()
            //                : System.Text.Json.JsonSerializer.Deserialize<List<string>>(src.RenovationGoalsJson)!
            //        ))
            //    .ForMember(dest => dest.Medias,
            //        opt => opt.MapFrom(src => src.Medias))
            //    .ForMember(dest => dest.Recommendations,
            //        opt => opt.MapFrom(src => src.Recommendations));

            //CreateMap<RenovationSimulation, SimulationAnalysisResultDto>()
            //    .ForMember(dest => dest.SimulationId,
            //        opt => opt.MapFrom(src => src.Id));




        }
    }
}
