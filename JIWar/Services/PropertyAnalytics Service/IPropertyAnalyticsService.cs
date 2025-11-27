using GEWAR.Models;
using Jiwar.DTOs.DistrictPriceHistoryDTOs;

namespace Jiwar.Services
{
    public interface IPropertyAnalyticsService
{
    Task<PropertyAnalytics> AnalyzePropertyAsync(Property property);
        Task<DistrictPriceHistoryDTO> GetDistrictPriceAnalytics(string district);

    }

}