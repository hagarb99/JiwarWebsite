using Jiwar.DTOs.DistrictPriceHistoryDTOs;
using Jiwar.DTOs.DistrictPricePointDTOs;
using Jiwar.Repositories.DistrictAnalyticService;

namespace Jiwar.Services.DistrictAnalyticService
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IAnalyticsRepository _repo;

        public AnalyticsService(IAnalyticsRepository repo)
        {
            _repo = repo;
        }

        public async Task<DistrictPriceHistoryDTO> GetDistrictPriceAnalytics(string district)
        {
            return new DistrictPriceHistoryDTO
            {
                Last1Year = (await _repo.GetDistrictHistoryAsync(district, 1))
                    .Select(x => new DistrictPricePointDTO
                    {
                        Date = x.RecordDate,
                        AvgPricePerMeter = x.AvgPricePerMeter
                    }).ToList(),

                Last3Years = (await _repo.GetDistrictHistoryAsync(district, 3))
                    .Select(x => new DistrictPricePointDTO
                    {
                        Date = x.RecordDate,
                        AvgPricePerMeter = x.AvgPricePerMeter
                    }).ToList(),

                Last5Years = (await _repo.GetDistrictHistoryAsync(district, 5))
                    .Select(x => new DistrictPricePointDTO
                    {
                        Date = x.RecordDate,
                        AvgPricePerMeter = x.AvgPricePerMeter
                    }).ToList()
            };
        }
    }

}
