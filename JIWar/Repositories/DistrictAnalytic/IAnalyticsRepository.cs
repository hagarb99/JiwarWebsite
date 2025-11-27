using Jiwar.Models;

namespace Jiwar.Repositories.DistrictAnalyticService
{
    public interface IAnalyticsRepository
    {
        Task<IEnumerable<DistrictPriceHistory>> GetDistrictHistoryAsync(string district, int years);
        Task<List<PropertyPriceHistory>> GetDistrictPriceHistoryAsync(string district);

    }

}
