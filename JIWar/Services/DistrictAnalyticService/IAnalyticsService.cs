using Jiwar.DTOs.DistrictPriceHistoryDTOs;

namespace Jiwar.Services.DistrictAnalyticService
{
    public interface IAnalyticsService
    {
         public Task<DistrictPriceHistoryDTO> GetDistrictPriceAnalytics(string district);
    }
}
