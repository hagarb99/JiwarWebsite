using GEWAR.Models;

public interface IRenovationSimulationRepository
{
    Task<RenovationSimulation?> GetByIdAsync(int id);
    Task<RenovationSimulation?> GetDraftByUserAsync(string userId);

    Task AddAsync(RenovationSimulation simulation);
    Task UpdateAsync(RenovationSimulation simulation);

    Task AddMediaAsync(SimulationMedia media);
    Task AddRecommendationsAsync(List<SimulationRecommendation> recommendations);

    Task SaveChangesAsync();
}
