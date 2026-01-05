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

                if (!doc.RootElement.TryGetProperty("recommendations", out var items))
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
            //recommendation = null!;

            //if (!item.TryGetProperty("category", out var catProp) ||
            //    !System.Enum.TryParse(catProp.GetString(), true, out RecommendationCategoryEnum category))
            //    return false;

            //if (!item.TryGetProperty("severity", out var sevProp) ||
            //    !System.Enum.TryParse(sevProp.GetString(), true, out RecommendationSeverityEnum severity))
            //    return false;

            //string title = item.TryGetProperty("title", out var titleProp) ? titleProp.GetString() ?? string.Empty : string.Empty;
            //string description = item.TryGetProperty("description", out var descProp) ? descProp.GetString() ?? string.Empty : string.Empty;

            //recommendation = new SimulationRecommendation
            //{
            //    RenovationSimulationID = simulationId,
            //    Category = category,
            //    Severity = severity,
            //    Title = title,
            //    Description = description,
            //    IsAIGenerated = true
            //};

            //return true;

            recommendation = null!;

            // project -> Title
            var title = item.TryGetProperty("project", out var projectProp)
                ? projectProp.GetString() ?? string.Empty
                : string.Empty;

            if (string.IsNullOrWhiteSpace(title))
                return false;

            // description
            var description = item.TryGetProperty("description", out var descProp)
                ? descProp.GetString() ?? string.Empty
                : string.Empty;

            // priority -> Severity
            var severity = RecommendationSeverityEnum.Medium;

            if (item.TryGetProperty("priority", out var priorityProp))
            {
                var priorityStr = priorityProp.GetString();

                if (!string.IsNullOrWhiteSpace(priorityStr))
                {
                  System.Enum.TryParse(priorityStr, true, out severity);
                }
            }

            recommendation = new SimulationRecommendation
            {
                RenovationSimulationID = simulationId,
                Title = title,
                Description = description,
                Severity = severity,
                Category = RecommendationCategoryEnum.General, // أو خريطة ذكية
                IsAIGenerated = true
            };

            return true;
        }
    }
}
