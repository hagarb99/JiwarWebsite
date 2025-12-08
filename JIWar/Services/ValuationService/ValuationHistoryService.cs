using Jiwar.DTOs.ValuationDTOs;
using Jiwar.Models.Valuation;
using Jiwar.Repositories.Valuation;
using Newtonsoft.Json;


namespace Jiwar.Services.ValuationService
{
    public class ValuationHistoryService : IValuationHistoryService
    {
        private readonly IValuationHistoryRepository _repo;

        public ValuationHistoryService(IValuationHistoryRepository repo)
        {
            _repo = repo;
        }

        public async Task SaveValuationAsync(ValuationSaveDTO dto)
        {
            var history = new ValuationHistory
            {
                UserId = dto.UserId,
                City = dto.City,
                Area = dto.Area,
                Bedrooms = dto.Bedrooms,
                Bathrooms = dto.Bathrooms,
                FinishType = dto.FinishType,
                View = dto.View,
                PropertyAge = dto.PropertyAge,
                MostLikelyPrice = dto.MostLikelyPrice,
                MinPrice = dto.MinPrice,
                MaxPrice = dto.MaxPrice,
                ConfidenceScore = dto.ConfidenceScore,
                FactorBreakdownJson = JsonConvert.SerializeObject(dto.FactorBreakdown)
            };

            await _repo.SaveAsync(history);
        }

        public async Task<IEnumerable<UserValuationListDTO>> GetMyValuationsAsync(string userId)
        {
            var list = await _repo.GetByUserAsync(userId);

            return list.Select(v => new UserValuationListDTO
            {
                Id = v.Id,
                City = v.City,
                Area = v.Area,
                MostLikelyPrice = v.MostLikelyPrice,
                MinPrice = v.MinPrice,
                MaxPrice = v.MaxPrice,
                CreatedAt = v.CreatedAt
            });
        }
    }
}
