using GEWAR.Models;
using Jiwar.Enum;

namespace Jiwar.DTOs
{
    public class SimulationRecommendationDto
{
    public RecommendationCategoryEnum Category { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public RecommendationSeverityEnum Severity { get; set; }
    public bool IsAIGenerated { get; set; }
}

}