namespace Jiwar.DTOs.PropertyComparisonDTO
{
    public class CategoryScoresDTO
    {
        public ScoreDTO PriceValue { get; set; } = new();
        public ScoreDTO Location { get; set; } = new();
        public ScoreDTO SpaceAndLayout { get; set; } = new();
        public ScoreDTO Features { get; set; } = new();
        public ScoreDTO InvestmentPotential { get; set; } = new();
    }
}
