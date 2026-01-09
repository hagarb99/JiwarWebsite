using Jiwar.DTOs.PropertyComparisonDTO;
using Jiwar.Services.AI.Comparison.Context;
using Jiwar.Services.AI.Comparison.Prompt;
using Jiwar.Services.AI.Enums;
using System.Text.Json;
using Jiwar.Enum;
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

        public async Task<AiComparisonResultDTO> CompareAsync(List<PropertyComparisonDTO> properties,PropertyComparisonUserType userType)
        {
            var prompt = PropertyComparisonContextBuilder.Build(properties, userType);

            var context = new AiRequestContext
            {
                Model = AiModelEnum.Gpt4oMini,
                Purpose = prompt,
                ResponseFormat = AiResponseFormat.Json,
                JsonSchema = PropertyComparisonJsonSchema.Schema
            };

            //var json = await _aiService.SendAsync(
            //    PropertyComparisonSystemPrompt.Prompt,
            //    context);

            //return JsonSerializer.Deserialize<AiComparisonResultDTO>(json)!;

            var json = await _aiService.SendAsync(
            PropertyComparisonSystemPrompt.Prompt,
            context);

            // تحقق إن response مش null أو فاضي
            if (string.IsNullOrWhiteSpace(json))
                throw new Exception("AI service returned empty response.");

            // اختياري: اطبع الـ response عشان تشوفي شكله
            Console.WriteLine(json);

            var result = JsonSerializer.Deserialize<AiComparisonResultDTO>(json);

            if (result == null)
                throw new Exception($"Failed to deserialize AI response: {json}");

            return result;

        }
    }
}

