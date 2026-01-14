using GEWAR.Models;

namespace Jiwar.Models.Valuation
{
    public class ValuationHistory : BaseModel
    {
        //public int Id { get; set; }
        public string UserId { get; set; } = null!;

        // Inputs
        public string City { get; set; } = string.Empty;
        public decimal Area { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public string FinishType { get; set; } = string.Empty;
        public string View { get; set; } = string.Empty;
        public int PropertyAge { get; set; }

        // Results
        public decimal MostLikelyPrice { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal ConfidenceScore { get; set; }

        // Factor breakdown stored as JSON
        public string FactorBreakdownJson { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual User User { get; set; } = null!;
    }
}
