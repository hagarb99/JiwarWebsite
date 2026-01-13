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

        //public async Task<AiComparisonResultDTO> CompareAsync(List<PropertyComparisonDTO> properties,PropertyComparisonUserType userType)
        //{
        //    var prompt = PropertyComparisonContextBuilder.Build(properties, userType);

        //    var context = new AiRequestContext
        //    {
        //        Model = AiModelEnum.Gpt4oMini,
        //        Purpose = prompt,
        //        ResponseFormat = AiResponseFormat.Json,
        //        JsonSchema = PropertyComparisonJsonSchema.Schema
        //    };

        //    //var json = await _aiService.SendAsync(
        //    //    PropertyComparisonSystemPrompt.Prompt,
        //    //    context);

        //    //return JsonSerializer.Deserialize<AiComparisonResultDTO>(json)!;

        //    var json = await _aiService.SendAsync(
        //    PropertyComparisonSystemPrompt.Prompt,
        //    context);

        //    // تحقق إن response مش null أو فاضي
        //    if (string.IsNullOrWhiteSpace(json))
        //        throw new Exception("AI service returned empty response.");

        //    // اختياري: اطبع الـ response عشان تشوفي شكله
        //    Console.WriteLine(json);

        //    var result = JsonSerializer.Deserialize<AiComparisonResultDTO>(json);

        //    if (result == null)
        //        throw new Exception($"Failed to deserialize AI response: {json}");

        //    return result;

        //}


        //    public async Task<AiComparisonResultDTO> CompareAsync(
        //List<PropertyComparisonDTO> properties,
        //PropertyComparisonUserType userType)
        //    {
        //        var prompt = PropertyComparisonContextBuilder.Build(properties, userType);

        //        var context = new AiRequestContext
        //        {
        //            Model = AiModelEnum.Gpt4o,
        //            Purpose = prompt,
        //            ResponseFormat = AiResponseFormat.Json,
        //            JsonSchema = PropertyComparisonJsonSchema.Schema
        //        };

        //        var json = await _aiService.SendAsync(
        //            PropertyComparisonSystemPrompt.Prompt,
        //            context);

        //        if (string.IsNullOrWhiteSpace(json))
        //            throw new Exception("AI service returned empty response.");

        //        Console.WriteLine(json);

        //        var options = new JsonSerializerOptions
        //        {
        //            PropertyNameCaseInsensitive = true
        //        };

        //        var result = JsonSerializer.Deserialize<AiComparisonResultDTO>(json, options);

        //        if (result == null)
        //            throw new Exception($"Failed to deserialize AI response: {json}");

        //        return result;
        //    }

        public async Task<AiComparisonResultDTO> CompareAsync(
    List<PropertyComparisonDTO> properties,
    PropertyComparisonUserType userType)
        {
            var prompt = PropertyComparisonContextBuilder.Build(properties, userType);

            var context = new AiRequestContext
            {
                Model = AiModelEnum.Gpt4o, // لو مش متاح، ممكن تغيّري لموديل آخر
                Purpose = prompt,
                ResponseFormat = AiResponseFormat.Json,
                JsonSchema = PropertyComparisonJsonSchema.Schema
            };

            //var json = await _aiService.SendAsync(
            //    PropertyComparisonSystemPrompt.Prompt,
            //    context);

            //if (string.IsNullOrWhiteSpace(json))
            //    throw new Exception("AI service returned empty response.");

            //Console.WriteLine("AI Response: " + json);

            //// ✅ تحقق أولاً لو الاستجابة تحتوي على error من OpenAI
            //if (json.Contains("\"error\""))
            //{
            //    throw new Exception($"AI service returned an error: {json}");
            //}

            //var options = new JsonSerializerOptions
            //{
            //    PropertyNameCaseInsensitive = true
            //};

            //// فك JSON بأمان
            //AiComparisonResultDTO? result = null;
            //try
            //{
            //    result = JsonSerializer.Deserialize<AiComparisonResultDTO>(json, options);
            //}
            //catch (JsonException ex)
            //{
            //    throw new Exception($"Failed to deserialize AI response. Invalid JSON: {json}", ex);
            //}

            //if (result == null)
            //    throw new Exception($"AI returned null result: {json}");

            //return result;

            var json = await _aiService.SendAsync(PropertyComparisonSystemPrompt.Prompt,context);

            // ✅ تحقق أولي
            if (string.IsNullOrWhiteSpace(json))
                throw new Exception("AI service returned empty response.");

            // (اختياري للديباج)
            Console.WriteLine("RAW AI RESPONSE:");
            Console.WriteLine(json);

            // 🔴 أهم سطر
            json = CleanJson(json);

            // (اختياري للديباج)
            Console.WriteLine("CLEAN JSON:");
            Console.WriteLine(json);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            AiComparisonResultDTO? result;
            try
            {
                result = JsonSerializer.Deserialize<AiComparisonResultDTO>(json, options);
            }
            catch (JsonException ex)
            {
                throw new Exception(
                    $"Failed to deserialize AI response. Invalid JSON: {json}", ex);
            }

            if (result == null)
                throw new Exception($"AI returned null result: {json}");

            return result;

        }

        private string CleanJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return json;

            json = json.Trim();

            // Remove ```json or ``` wrappers if AI returned markdown
            if (json.StartsWith("```"))
            {
                json = json
                    .Replace("```json", "", StringComparison.OrdinalIgnoreCase)
                    .Replace("```", "")
                    .Trim();
            }

            // Optional: remove BOM or invisible characters
            json = json.Trim('\uFEFF', '\u200B');

            return json;
        }
    }
}

