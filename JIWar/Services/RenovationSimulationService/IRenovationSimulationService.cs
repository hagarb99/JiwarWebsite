using GEWAR.Models;
using Jiwar.DTOs;

public interface IRenovationSimulationService
{
    Task<int> StartSimulationAsync(string userId, int propertyId, decimal budgetMin, decimal budgetMax, string goalsJson);

    Task UpdateDetailsAsync(
        int simulationId,
        decimal size,
        int rooms,
        int bathrooms,
        string condition);

    Task UploadMediaAsync(
        int simulationId,
        SimulationMediaTypeEnum type,
        string fileUrl);

    Task SetGoalsAndBudgetAsync(
        int simulationId,
        List<string> goals,
        decimal? budgetMin,
        decimal? budgetMax);

    Task CompleteSimulationAsync(int simulationId);

    Task GenerateRecommendationsAsync(int simulationId);

    Task<SimulationResultDto> GetResultsAsync(int simulationId);
}
