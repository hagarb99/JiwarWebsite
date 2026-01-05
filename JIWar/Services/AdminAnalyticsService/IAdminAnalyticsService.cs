using Jiwar.DTOs;
using Jiwar.DTOs.AccountDTOs.EditProfileDtos;

namespace Jiwar.Services
{
    public interface IAdminAnalyticsService
{
 //   Task<AdminAnalyticsDTO> GetDashboardDataAsync();
        Task<AdminAnalyticsDTO> GetAnalyticsAsync();

       
    }
}

