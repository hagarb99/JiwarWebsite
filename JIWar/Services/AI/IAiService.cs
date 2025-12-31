using Jiwar.Services.AI.Enums;

namespace Jiwar.Services.AI
{
    public interface IAiService
    {
        // ============================
        // TEXT (GPT-4o / GPT-4o-mini)
        // ============================
        Task<string> SendAsync(
            string prompt,
            AiRequestContext context);

        // ============================
        // VISION (GPT-4o)
        // ============================
        Task<string> AnalyzeImagesAsync(
            string prompt,
            List<string> imageUrls,
            AiModelEnum model);

        // ============================
        // IMAGE GENERATION (DALL·E-3)
        // ============================
        Task<List<string>> GenerateImagesAsync(
            string prompt,
            AiModelEnum model);
    }
}
