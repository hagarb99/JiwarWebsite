namespace Jiwar.DTOs.PropertyComparisonDTO
{
    public class AiComparisonResultDTO
    {
        // Overall comparison
        public int BestPropertyId { get; set; }
        public string Summary { get; set; } = string.Empty;
        public List<AiPropertyScoreBreakdownDTO> Scores { get; set; } = new();

    }
}
