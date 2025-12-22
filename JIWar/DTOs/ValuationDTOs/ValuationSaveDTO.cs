namespace Jiwar.DTOs.ValuationDTOs
{
  
    
    public class ValuationSaveDTO
    {

        public string UserId { get; set; } = null!;
        public string City { get; set; }
        public decimal Area { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public string FinishType { get; set; }
        public string View { get; set; }
        public int PropertyAge { get; set; }

        public decimal MostLikelyPrice { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int ConfidenceScore { get; set; }
        public Dictionary<string, decimal> FactorBreakdown { get; set; }
    }

}
