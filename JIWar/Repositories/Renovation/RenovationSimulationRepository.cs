using GEWAR;
using GEWAR.Models;
using GEWAR.Models.Jiwar.Enum;
using Microsoft.EntityFrameworkCore;

public class RenovationSimulationRepository : IRenovationSimulationRepository
{
    private readonly GiwarContext _context;

    public RenovationSimulationRepository(GiwarContext context)
    {
        _context = context;
    }

    public async Task<RenovationSimulation?> GetByIdAsync(int id)
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

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

