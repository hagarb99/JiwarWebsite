using GEWAR.Models;
using Jiwar.Models;   // مكان وجود Subscription Model
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jiwar.Repositories.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<decimal> GetTotalRevenueAsync();
        Task<decimal> GetTodayRevenueAsync();
        Task<decimal> GetWeekRevenueAsync();
        Task<decimal> GetMonthRevenueAsync();
    }

}
