namespace Jiwar.DTOs.ValuationDTOs
{
    public class ValuationResultDTO
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MostLikelyPrice { get; set; }
        public int ConfidenceScore { get; set; } // 0–100
        public Dictionary<string, decimal> FactorBreakdown { get; set; }
    }
}
