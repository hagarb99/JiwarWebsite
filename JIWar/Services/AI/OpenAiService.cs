using Jiwar.Services.AI.Enums;
using System.Text;
using System.Text.Json;


namespace Jiwar.Services.AI
{
    public class OpenAiService : IAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenAiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")
                ?? throw new Exception("OpenAI API Key not found");
        }

        public async Task<string> SendAsync(string prompt, AiRequestContext context)
        {
            var model = ResolveModel(context.Model);

            var request = new
            {
                model = model,
                messages = new[]
                {
                new { role = "system", content = context.Purpose },
                new { role = "user", content = prompt }
            }
            };

            var httpRequest = new HttpRequestMessage(
                HttpMethod.Post,
                "https://api.openai.com/v1/chat/completions"
            );

            httpRequest.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

            httpRequest.Content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            return doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();
        }

        private string ResolveModel(AiModelEnum model)
        {
            return model switch
            {
                AiModelEnum.Gpt4o => "gpt-4o",
                AiModelEnum.Gpt4oMini => "gpt-4o-mini",
                AiModelEnum.DallE3 => "dall-e-3",
                _ => throw new Exception("Unsupported AI Model")
            };
        }
    }
}
