namespace Jiwar.DTOs.PropertyComparisonDTO
{
    public class AiPropertyScoreBreakdownDTO
    {
        public int PropertyId { get; set; }

        public int PriceValue { get; set; }
        public int Location { get; set; }
        public int Space { get; set; }
        public int InvestmentPotential { get; set; }
        public int Comfort { get; set; }

        public int TotalScore { get; set; }
    }
}
