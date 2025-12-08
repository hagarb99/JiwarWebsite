using Jiwar.DTOs.ValuationDTOs;

namespace Jiwar.Services.ValuationService
{
    public class ValuationService : IValuationService
    {
        public ValuationResultDTO CalculateValuation(ValuationRequestDTO r)
        {
            decimal basePrice = GetCityBasePrice(r.City);

            var factors = new Dictionary<string, decimal>();

            factors["AreaInfluence"] = r.Area * 1500;
            factors["BedroomsInfluence"] = r.Bedrooms * 50000;
            factors["BathroomsInfluence"] = r.Bathrooms * 30000;
            factors["FinishInfluence"] = GetFinishMultiplier(r.FinishType);
            factors["ViewInfluence"] = r.View?.ToLower() == "sea" ? 200000 : 0;

            decimal price = basePrice + factors.Values.Sum();

            return new ValuationResultDTO
            {
                MostLikelyPrice = price,
                MinPrice = price * 0.9m,
                MaxPrice = price * 1.1m,
                ConfidenceScore = CalculateConfidence(r),
                FactorBreakdown = factors
            };
        }

        // -----------------------------
        // Helpers (New Methods Added)
        // -----------------------------
        private decimal GetCityBasePrice(string city)
        {
            if (string.IsNullOrEmpty(city)) return 10000;

            return city.ToLower() switch
            {
                "new cairo" => 20000,
                "6 october" => 15000,
                "nasr city" => 18000,
                _ => 12000
            };
        }

        private decimal GetFinishMultiplier(string finish)
        {
            if (string.IsNullOrEmpty(finish)) return 1.0m;

            return finish.ToLower() switch
            {
                "luxury" => 1.30m,
                "super lux" => 1.20m,
                "semi finished" => 0.90m,
                "core & shell" => 0.75m,
                _ => 1.0m
            };
        }

        private int CalculateConfidence(ValuationRequestDTO r)
        {
            int score = 0;

            if (r.Area >= 80) score += 25;
            if (r.Bedrooms >= 3) score += 25;
            if (!string.IsNullOrEmpty(r.City)) score += 25;
            if (!string.IsNullOrEmpty(r.FinishType)) score += 25;

            return score;  
        }


    }
}
