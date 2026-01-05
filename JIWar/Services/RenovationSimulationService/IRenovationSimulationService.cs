using GEWAR.Models;
using Jiwar.DTOs;

public interface IRenovationSimulationService
{
    Task<int> StartSimulationAsync(StartSimulationDto dto ,string userId );

    Task UpdateDetailsAsync(int simulationId, UpdateSimulationDetailsDto dto);

    Task UploadMediaAsync(int simulationId, UploadSimulationMediaDto dto);

    Task SetGoalsAndBudgetAsync(int simulationId, SimulationGoalsDto dto);

    Task CompleteSimulationAsync(int simulationId);

    Task GenerateRecommendationsAsync(int simulationId , int? propertyId = null);

    Task<SimulationResultDto> GetResultsAsync(int simulationId);

}
