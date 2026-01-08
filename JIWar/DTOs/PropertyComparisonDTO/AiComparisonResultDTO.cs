namespace Jiwar.DTOs.PropertyComparisonDTO
{
    public class AiComparisonResultDTO
    {
        // Overall comparison
        public int BestPropertyId { get; set; }
        public string Summary { get; set; } = string.Empty;

        // Property-specific info
        public int PropertyId { get; set; }

        // Price Value
        public double PriceValueScore { get; set; }
        public string PriceValueDescription { get; set; } = string.Empty;

        // Location
        public double LocationScore { get; set; }
        public string LocationDescription { get; set; } = string.Empty;

        // Space & Layout
        public double SpaceAndLayoutScore { get; set; }
        public string SpaceAndLayoutDescription { get; set; } = string.Empty;

        // Features
        public double FeaturesScore { get; set; }
        public string FeaturesDescription { get; set; } = string.Empty;

        // Investment Potential
        public double InvestmentPotentialScore { get; set; }
        public string InvestmentPotentialDescription { get; set; } = string.Empty;

        // Overall score & reason
        public double TotalScore { get; set; }
        public string OverallReason { get; set; } = string.Empty;
    }
}
