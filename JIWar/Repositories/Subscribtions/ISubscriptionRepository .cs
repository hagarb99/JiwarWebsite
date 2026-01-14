using GEWAR.Models;
using Jiwar.Models; // Subscription model
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jiwar.Repositories.Interfaces
{
    public interface ISubscriptionRepository
    {
        // ===============================
        // CRUD Methods
        // ===============================
        Task<IEnumerable<Subscription>> GetAllAsync();
        Task<Subscription?> GetByIdAsync(int id);
        Task AddAsync(Subscription subscription);
        void Update(Subscription subscription);
        void Remove(Subscription subscription);
        Task SaveAsync();

        // ===============================
        // Revenue Methods
        // ===============================
        Task<decimal> GetTotalRevenueAsync();
        Task<decimal> GetTodayRevenueAsync();
        Task<decimal> GetWeekRevenueAsync();
        Task<decimal> GetMonthRevenueAsync();
        Task<bool> HasActiveSubscriptionAsync(string userId);

    }
}
