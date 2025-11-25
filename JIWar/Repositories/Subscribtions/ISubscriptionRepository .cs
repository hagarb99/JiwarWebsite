using GEWAR.Models;
using Jiwar.Models;   // مكان وجود Subscription Model
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jiwar.Repositories.Interfaces
{
    public interface ISubscriptionRepository : IGenericRepository<Subscription>
    {
        Task<IEnumerable<Subscription>> GetAllSubscriptionsAsync();
        Task<Subscription> GetSubscriptionByIdAsync(int id);
    }
}
