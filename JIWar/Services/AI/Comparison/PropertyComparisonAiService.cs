using Jiwar.DTOs.PropertyComparisonDTO;
using Jiwar.Services.AI.Comparison.Context;
using Jiwar.Services.AI.Comparison.Prompt;
using Jiwar.Services.AI.Enums;
using Jiwar.Enum;
using System.Text.Json;
using static Jiwar.Services.AI.Comparison.IPropertyComparisonAiService;

namespace Jiwar.Services.AI.Comparison
{
    public class PropertyComparisonAiService : IPropertyComparisonAiService
    {
        private readonly IAiService _aiService;

        public PropertyComparisonAiService(IAiService aiService)
        {
            _aiService = aiService;
        }

        public async Task<AiComparisonResultDTO> CompareAsync(
            List<PropertyComparisonDTO> properties,
            PropertyComparisonUserType userType)
        {
            var prompt = PropertyComparisonContextBuilder.Build(properties, userType);

            var context = new AiRequestContext
            {
                Model = AiModelEnum.Gpt4o,
                Purpose = prompt,
                ResponseFormat = AiResponseFormat.Json,
                JsonSchema = PropertyComparisonJsonSchema.Schema
            };

            var json = await _aiService.SendAsync(
                PropertyComparisonSystemPrompt.Prompt,
                context);

            if (string.IsNullOrWhiteSpace(json))
                throw new Exception("AI service returned empty response.");

            // Debug (can be removed in production)
            Console.WriteLine("RAW AI RESPONSE:");
            Console.WriteLine(json);

            // 🔴 Important: clean markdown / extra chars
            json = CleanJson(json);

            Console.WriteLine("CLEAN JSON:");
            Console.WriteLine(json);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                var result = JsonSerializer.Deserialize<AiComparisonResultDTO>(json, options);

                if (result == null)
                    throw new Exception("AI returned null result.");

                return result;
            }
            catch (JsonException ex)
            {
                throw new Exception(
                    $"Failed to deserialize AI response. Invalid JSON: {json}", ex);
            }
        }

        private string CleanJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return json;

            json = json.Trim();

            // Remove ```json / ``` markdown wrappers
            if (json.StartsWith("```"))
            {
                json = json
                    .Replace("```json", "", StringComparison.OrdinalIgnoreCase)
                    .Replace("```", "")
                    .Trim();
            }

            // Remove invisible characters (BOM, zero-width space)
            json = json.Trim('\uFEFF', '\u200B');

            return json;
        }
    }
}
