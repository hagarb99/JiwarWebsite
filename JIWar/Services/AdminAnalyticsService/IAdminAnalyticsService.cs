using Jiwar.DTOs;

namespace Jiwar.Services
{
    public interface IAdminAnalyticsService
{
    Task<AdminAnalyticsDTO> GetDashboardDataAsync();
}
}

