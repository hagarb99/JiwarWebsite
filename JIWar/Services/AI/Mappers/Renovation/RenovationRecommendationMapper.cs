using GEWAR.Models;
using System.Text.Json;
using GEWAR.Models.Jiwar.Enum;
using Jiwar.Enum;
using Jiwar.Services.AI.Mappers.Renovation;


namespace Jiwar.Services.AI.Mappers.Renovation
{
    public static class RenovationRecommendationMapper
    {
        public static List<SimulationRecommendation> Map(
            string aiResponse,
            int simulationId)
        {
            var result = new List<SimulationRecommendation>();

            using var document = JsonDocument.Parse(aiResponse);

            if (!document.RootElement.TryGetProperty("recommendations", out var recs))
                return result;

            foreach (var item in recs.EnumerateArray())
            {
                result.Add(new SimulationRecommendation
                {
                    RenovationSimulationID = simulationId,
                    Category = System.Enum.Parse<RecommendationCategoryEnum>(
                        item.GetProperty("category").GetString()!,
                        true),
                    Title = item.GetProperty("title").GetString()!,
                    Description = item.GetProperty("description").GetString()!,
                    Severity = System.Enum.Parse<RecommendationSeverityEnum>(
                        item.GetProperty("severity").GetString()!,
                        true),
                    IsAIGenerated = true
                });
            }

            return result;
        }
    }
}