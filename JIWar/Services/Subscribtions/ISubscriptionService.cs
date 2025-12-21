using JIWar.PropertyOwner;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jiwar.Services
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<SubscriptionDetailsDTO>> GetAllAsync();
        Task<SubscriptionDetailsDTO> GetByIdAsync(int id);
        Task<SubscriptionDetailsDTO> CreateAsync(SubscriptionCreateDTO dto, string userId);

        //Task<SubscriptionDetailsDTO> CreateAsync(SubscriptionCreateDTO dto);
        Task<bool> UpdateAsync(SubscriptionUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
