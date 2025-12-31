
namespace Jiwar.DTOs.RenovationDTOs
{
    public class SimulationAnalysisResultDto
    {
        public int SimulationId { get; set; }
        public string Condition { get; set; }
        public List<string> Issues { get; set; }
        public List<SimulationRecommendationDto> Recommendations { get; set; }

    }
}
