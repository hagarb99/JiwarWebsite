using Jiwar.Models.Valuation;

namespace Jiwar.Repositories.Valuation
{
    public interface IValuationHistoryRepository
    {
        Task SaveAsync(ValuationHistory history);
        Task<IEnumerable<ValuationHistory>> GetByUserAsync(string userId);
    }
}
