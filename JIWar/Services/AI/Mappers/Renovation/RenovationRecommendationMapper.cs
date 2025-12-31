using GEWAR.Models;
using System.Text.Json;
using GEWAR.Models.Jiwar.Enum;
using Jiwar.Enum;


namespace Jiwar.Services.AI.Mappers.Renovation
{
    public static class RenovationRecommendationMapper
    {
        public static List<SimulationRecommendation> Map(
            string aiJson,
            int simulationId)
        {
            var recommendations = new List<SimulationRecommendation>();

            if (string.IsNullOrWhiteSpace(aiJson))
                return recommendations;

            using var doc = JsonDocument.Parse(aiJson);

            if (!doc.RootElement.TryGetProperty("recommendations", out var items))
                return recommendations;

            foreach (var item in items.EnumerateArray())
            {
                if (!TryParse(item, simulationId, out var rec))
                    continue;

                recommendations.Add(rec);
            }

            return recommendations;
        }

          private static bool TryParse(
            JsonElement item,
            int simulationId,
            out SimulationRecommendation recommendation)
        {
            recommendation = null!;

            if (!System.Enum.TryParse(
                    item.GetProperty("category").GetString(),
                    true,
                    out RecommendationCategoryEnum category))
                return false;

            if (!System.Enum.TryParse(
                    item.GetProperty("severity").GetString(),
                    true,
                    out RecommendationSeverityEnum severity))
                return false;

            recommendation = new SimulationRecommendation
            {
                RenovationSimulationID = simulationId,
                Category = category,
                Severity = severity,
                Title = item.GetProperty("title").GetString() ?? string.Empty,
                Description = item.GetProperty("description").GetString() ?? string.Empty,
                IsAIGenerated = true
            };

            return true;
        }
    }
}
