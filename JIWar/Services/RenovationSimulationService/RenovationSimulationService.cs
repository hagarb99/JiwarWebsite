using GEWAR.Models;
using GEWAR.Models.Jiwar.Enum;
using Jiwar.DTOs;
using Jiwar.Services.AI;
using Jiwar.Services.AI.Enums;
using Jiwar.Services.AI.Prompts.Renovations;
using System.Text.Json;
using AutoMapper;
using Jiwar.Models;
using Jiwar.Services.AI.Mappers.Renovation;
public class RenovationSimulationService : IRenovationSimulationService
{
    private readonly IRenovationSimulationRepository _repo;
    private readonly IMapper _mapper;
    private readonly IAiService _aiService;

    public RenovationSimulationService(IRenovationSimulationRepository repo, IMapper mapper ,IAiService aiService)
    {
        _repo = repo;
        _mapper = mapper;
        _aiService = aiService;
    } 

    // 1️⃣ Start
    public async Task<int> StartSimulationAsync(
    StartSimulationDto dto,
    string userId)
{
    // 1️⃣ check existing draft
    var draft = await _repo.GetDraftByUserAsync(userId);
    if (draft != null)
        return draft.Id;

    // 2️⃣ create entity
    var simulation = new RenovationSimulation
    {
        UserID = userId,
        PropertyID = dto.PropertyId,
        BudgetMin = dto.BudgetMin,
        BudgetMax = dto.BudgetMax,
        RenovationGoalsJson = JsonSerializer.Serialize(dto.GoalsJson),
        Status = SimulationStatusEnum.Draft
    };

    await _repo.AddAsync(simulation);
    await _repo.SaveChangesAsync();

    return simulation.Id;
}
    // 2️⃣ Details
    public async Task UpdateDetailsAsync(
    int simulationId,
    UpdateSimulationDetailsDto dto)
{
    var simulation = await _repo.GetByIdAsync(simulationId)
        ?? throw new Exception("Simulation not found");

    var details = _mapper.Map<SimulationDetails>(dto);
    details.RenovationSimulationID = simulationId;

    await _repo.AddSimulationDetailsAsync(details , simulationId);
    await _repo.SaveChangesAsync();
}


    // 3️⃣ Media
    public async Task UploadMediaAsync(
    int simulationId,
    UploadSimulationMediaDto dto)
{
    var media = _mapper.Map<SimulationMedia>(dto);
    media.RenovationSimulationID = simulationId;

    await _repo.AddMediaAsync(media);
    await _repo.SaveChangesAsync();
}


    // 4️ Goals & Budget
    public async Task SetGoalsAndBudgetAsync(
    int simulationId,
    SimulationGoalsDto dto)
{
    var simulation = await _repo.GetByIdAsync(simulationId)
        ?? throw new Exception("Simulation not found");

    simulation.RenovationGoalsJson =
        JsonSerializer.Serialize(dto.Goals);

    simulation.BudgetMin = dto.BudgetMin;
    simulation.BudgetMax = dto.BudgetMax;

    await _repo.UpdateAsync(simulation);
    await _repo.SaveChangesAsync();
}


    // 5️ Results
    public async Task CompleteSimulationAsync(int simulationId)
    {
        var simulation = await _repo.GetByIdAsync(simulationId)
            ?? throw new Exception("Simulation not found");
    }

//        await _repo.UpdateAsync(simulation);
//        await _repo.SaveChangesAsync();
//    }


//    public async Task GenerateRecommendationsAsync(int simulationId)
//    {
//        var simulation = await _repo.GetByIdAsync(simulationId)
//            ?? throw new Exception("Simulation not found");


public async Task GenerateRecommendationsAsync(int simulationId)
{
    var simulation = await _repo.GetWithResultsAsync(simulationId)
        ?? throw new Exception("Simulation not found");

    var details = await _repo.GetDetailsBySimulationIdAsync(simulationId)
        ?? throw new Exception("Simulation details not found");

    var context = RenovationContextBuilder.Build(simulation, details);

    var aiResponse = await _aiService.SendAsync(
        RenovationSystemPrompt.Build,
        new AiRequestContext { Purpose = context });

    var recommendations =
        RenovationRecommendationMapper
            .Map(aiResponse, simulationId);

    await _repo.AddRecommendationsAsync(recommendations);

    simulation.Status = SimulationStatusEnum.Analyzed;
    await _repo.SaveChangesAsync();
}

    public async Task<SimulationResultDto> GetResultsAsync(int simulationId)
{
    var simulation = await _repo.GetWithResultsAsync(simulationId)
        ?? throw new Exception("Simulation not found");

    var result = _mapper.Map<SimulationResultDto>(simulation);

    // Logic هنا مش في المابر
    result.Goals = string.IsNullOrWhiteSpace(simulation.RenovationGoalsJson)
        ? new List<string>()
        : JsonSerializer.Deserialize<List<string>>(
            simulation.RenovationGoalsJson)!;

    return result;
}

   
}
