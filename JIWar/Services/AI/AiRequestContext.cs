using Jiwar.Services.AI.Enums;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Jiwar.Services.AI
{
    public class AiRequestContext
    {
        // ============================
        // Which AI model to use
        // ============================
        public AiModelEnum Model { get; set; }

        // ============================
        // System role / intent
        // (e.g. "You are a renovation expert AI...")
        // ============================
        public string Purpose { get; set; } = string.Empty;

        // ============================
        // Expected response format
        // (PlainText / Json)
        // ============================
        public AiResponseFormat ResponseFormat { get; set; }
            = AiResponseFormat.PlainText;

        // ============================
        // Strict JSON schema (anti-hallucination)
        // ============================
        public string? JsonSchema { get; set; }

        // ============================
        // Retry & fallback support
        // ============================
        public int MaxRetries { get; set; } = 1;
    }
}
