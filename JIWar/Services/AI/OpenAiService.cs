using Jiwar.Models;
using Jiwar.Services.AI.Enums;
using System.Text;
using System.Text.Json;

namespace Jiwar.Services.AI
{
    public class OpenAiService : IAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        private const string ChatEndpoint = "https://api.openai.com/v1/chat/completions";
        private const string ImageEndpoint = "https://api.openai.com/v1/images/generations";

        public OpenAiService(HttpClient httpClient, IConfiguration configuration)
       {
    _httpClient = httpClient;
    _apiKey = configuration["OpenAI:ApiKey"]
        ?? throw new Exception("OpenAI API Key not found");
    }


        // ============================
        // TEXT (GPT-4o / GPT-4o-mini)
        // ============================
        public async Task<string> SendAsync(string prompt, AiRequestContext context)
{
    var request = new
    {
        model = ResolveModel(context.Model),
        messages = new[]
        {
            new { role = "system", content = context.Purpose },
            new { role = "user", content = prompt }
        }
    };

    var json = await SendRawAsync(request, ChatEndpoint);

    using var doc = JsonDocument.Parse(json);
    return doc.RootElement
        .GetProperty("choices")[0]
        .GetProperty("message")
        .GetProperty("content")
        .GetString()!;
}

        // ============================
        // VISION (GPT-4o)
        // ============================
        public async Task<string> AnalyzeImagesAsync(
    string prompt,
    List<string> imageUrls,
    AiModelEnum model)
{
    var request = new
    {
        model = ResolveModel(model),
        messages = new[]
        {
            new
            {
                role = "user",
                content = BuildVisionContent(prompt, imageUrls)
            }
        }
    };

    var json = await SendRawAsync(request, ChatEndpoint);

    using var doc = JsonDocument.Parse(json);
    return doc.RootElement
        .GetProperty("choices")[0]
        .GetProperty("message")
        .GetProperty("content")
        .GetString()!;
}


        // ============================
        // IMAGE GENERATION (DALL·E-3)
        // ============================
        public async Task<List<string>> GenerateImagesAsync(
            string prompt,
            AiModelEnum model)
        {
            var request = new
            {
                model = ResolveModel(model),
                prompt = prompt,
                size = "1024x1024",
                quality = "hd",
                n = 1
            };

            var json = await SendRawAsync(request, ImageEndpoint);

            using var doc = JsonDocument.Parse(json);

            return doc.RootElement
                .GetProperty("data")
                .EnumerateArray()
                .Select(x => x.GetProperty("url").GetString()!)
                .ToList();
        }

        // ============================
        // LOW LEVEL HTTP
        // ============================
        private async Task<string> SendRawAsync(object body, string endpoint)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, endpoint);

            httpRequest.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

            httpRequest.Content = new StringContent(
                JsonSerializer.Serialize(body),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.SendAsync(httpRequest);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"OpenAI API Error: {error}");
            }

            return await response.Content.ReadAsStringAsync();
        }

        // ============================
        // HELPERS
        // ============================
        private static object[] BuildVisionContent(
            string prompt,
            List<string> imageUrls)
        {
            var content = new List<object>
            {
                new { type = "text", text = prompt }
            };

            foreach (var url in imageUrls)
            {
                content.Add(new
                {
                    type = "image_url",
                    image_url = new { url }
                });
            }

            return content.ToArray();
        }

        private static string ResolveModel(AiModelEnum model)
        {
            return model switch
            {
                /*AiModelEnum.Gpt4o => "gpt-4o"*/
                AiModelEnum.Gpt4oMini => "gpt-4o-mini",
                AiModelEnum.DallE3 => "dall-e-3",
                _ => throw new Exception("Unsupported AI Model")
            };
        }
    }
}

