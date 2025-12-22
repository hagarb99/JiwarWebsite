using GEWAR.Models;
using Jiwar.DTOs;

public interface IRenovationSimulationService
{
    Task<int> StartSimulationAsync(string userId, int propertyId);

    Task UpdateDetailsAsync(int simulationId, decimal size, int rooms, int bathrooms, string condition);

    Task UploadMediaAsync(int simulationId, SimulationMediaTypeEnum type, string fileUrl);

    Task SetGoalsAndBudgetAsync(
        int simulationId,
        List<string> goals,
        decimal? budgetMin,
        decimal? budgetMax);

    Task CompleteSimulationAsync(int simulationId);

    //Task<RenovationSimulation?> GetResultsAsync(int simulationId);
    Task<SimulationRecommendationDto> GetResultsAsync(int simulationId);

}

