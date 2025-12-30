using GEWAR;
using GEWAR.Models;
using GEWAR.Models.Jiwar.Enum;
using Jiwar.Models;
using Microsoft.EntityFrameworkCore;

public class RenovationSimulationRepository : IRenovationSimulationRepository
{
    private readonly GiwarContext _context;

    public RenovationSimulationRepository(GiwarContext context)
    {
        _context = context;
    }

    // 🔐 Ownership
    public async Task<RenovationSimulation?> GetByIdForUserAsync(int id, string userId)
    {
        return await _context.RenovationSimulations
            .Include(x => x.Medias)
            .Include(x => x.Recommendations)
            .FirstOrDefaultAsync(x => x.Id == id && x.UserID == userId);
    }

    // 📦 Reads
    public async Task<RenovationSimulation?> GetByIdAsync(int id)
    {
        return await _context.RenovationSimulations
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<RenovationSimulation?> GetWithResultsAsync(int id)
    {
        return await _context.RenovationSimulations
            .Include(x => x.Medias)
            .Include(x => x.Recommendations)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<RenovationSimulation?> GetDraftByUserAsync(string userId)
    {
        return await _context.RenovationSimulations
            .FirstOrDefaultAsync(x =>
                x.UserID == userId &&
                x.Status == SimulationStatusEnum.Draft);
    }

    // ✍️ Writes
    public async Task AddAsync(RenovationSimulation simulation)
    {
        await _context.RenovationSimulations.AddAsync(simulation);
    }

    public Task UpdateAsync(RenovationSimulation simulation)
    {
        _context.RenovationSimulations.Update(simulation);
        return Task.CompletedTask;
    }

    public async Task AddMediaAsync(SimulationMedia media)
    {
        await _context.SimulationMedias.AddAsync(media);
    }

    public async Task AddRecommendationsAsync(List<SimulationRecommendation> recommendations)
    {
        await _context.SimulationRecommendations.AddRangeAsync(recommendations);
    }

    public async Task AddRenovationProjectAsync(RenovationProject project)
    {
        await _context.RenovationProjects.AddAsync(project);
    }

    public async Task AddSimulationDetailsAsync(SimulationDetails details)
    {
        await _context.SimulationDetails.AddAsync(details);
    }


    // 💾 Unit of Work
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    // 🔁 Transactions
    public async Task BeginTransactionAsync()
    {
        await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _context.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }
}
