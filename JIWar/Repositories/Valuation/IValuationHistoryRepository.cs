using Jiwar.Models.Valuation;

public interface IValuationHistoryRepository
{
    Task SaveAsync(ValuationHistory history);
    Task<IEnumerable<ValuationHistory>> GetByUserAsync(string userId);

    // Admin Analytics
    Task<int> GetTotalValuationsAsync();
    Task<int> GetValuationsTodayAsync();
    Task<int> GetValuationsThisWeekAsync();
    Task<int> GetValuationsThisMonthAsync();
}
