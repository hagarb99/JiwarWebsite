using GEWAR.Models;
using System.Text.Json;
//using GEWAR.Models.Jiwar.Enum;
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

            // 1️⃣ حماية أولى: null أو فاضي
            if (string.IsNullOrWhiteSpace(aiJson))
                return recommendations;

            // 2️⃣ حماية تانية: مش JSON أصلاً
            var trimmed = aiJson.TrimStart();
            if (!trimmed.StartsWith("{") && !trimmed.StartsWith("["))
                return recommendations;

            try
            {
                // 3️⃣ السطر الخطر بقى آمن
                using var doc = JsonDocument.Parse(aiJson);

                if (!doc.RootElement.TryGetProperty("renovation_recommendations", out var items))
                    return recommendations;

                foreach (var item in items.EnumerateArray())
                {
                    if (!TryParse(item, simulationId, out var rec))
                        continue;

                    recommendations.Add(rec);
                }
            }
            catch (JsonException ex)
            {
                // 4️⃣ لو AI رجّع JSON مضروب
                // log لو حابة، لكن متوقفيش السيستم
                Console.WriteLine($"Invalid AI JSON: {ex.Message}");
                return recommendations;
            }

            return recommendations;
        }

        private static bool TryParse(
        JsonElement item,
        int simulationId,
        out SimulationRecommendation recommendation)
            {
                recommendation = null!;

                // 🔐 mandatory fields
                if (!item.TryGetProperty("title", out var titleProp) ||
                    !item.TryGetProperty("description", out var descProp))
                    return false;

                var severityString =
                    item.TryGetProperty("severity", out var sevProp)
                        ? sevProp.GetString()
                        : "Low";

                var categoryString =
                        item.TryGetProperty("category", out var categoryprop)
                            ? categoryprop.GetString()
                            : "Design";

                System.Enum.TryParse<RecommendationSeverityEnum>(
                        severityString,
                        ignoreCase: true,
                        out var severity);

                System.Enum.TryParse<RecommendationCategoryEnum>(
                        categoryString,
                        ignoreCase: true,
                        out var category);

                recommendation = new SimulationRecommendation
                    {
                        RenovationSimulationID = simulationId,
                        Category = category,
                        Title = titleProp.GetString() ?? "",
                        Description = descProp.GetString() ?? "",
                        Severity = severity,
                        IsAIGenerated = item.TryGetProperty("IsAIGenerated", out var aiProp)
                            && aiProp.GetBoolean()
                    };

                    return true;
                }

        }
}
