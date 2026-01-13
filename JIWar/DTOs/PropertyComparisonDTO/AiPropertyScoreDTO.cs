namespace Jiwar.DTOs.PropertyComparisonDTO
{
    public class AiPropertyScoreDTO
    {
        public int PropertyId { get; set; }
        public CategoryScoresDTO CategoryScores { get; set; } = new();
        public double TotalScore { get; set; }
        public string OverallReason { get; set; } = string.Empty;
    }
}
