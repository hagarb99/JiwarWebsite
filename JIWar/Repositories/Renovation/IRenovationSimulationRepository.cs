using GEWAR.Models;
using Jiwar.Models;
using Microsoft.EntityFrameworkCore;

public interface IRenovationSimulationRepository
{
    // 🔐 Ownership
    Task<RenovationSimulation?> GetByIdForUserAsync(int id, string userId);

    // 📦 Reads
    Task<RenovationSimulation?> GetByIdAsync(int id);
    Task<RenovationSimulation?> GetWithResultsAsync(int id);
    Task<RenovationSimulation?> GetDraftByUserAsync(string userId);

    // ✍️ Writes
    Task AddAsync(RenovationSimulation simulation);
    Task UpdateAsync(RenovationSimulation simulation);
    Task AddMediaAsync(SimulationMedia media);
    Task AddRecommendationsAsync(List<SimulationRecommendation> recommendations);
    Task AddRenovationProjectAsync(RenovationProject project);

    Task AddSimulationDetailsAsync(SimulationDetails details);
    


    // 💾 Unit Of Work
    Task SaveChangesAsync();

    // 🔁 Transactions (AI Safety)
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
