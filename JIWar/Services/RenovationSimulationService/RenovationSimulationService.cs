using GEWAR.Models;
using GEWAR.Models.Jiwar.Enum;
using Jiwar.DTOs;
using Jiwar.Services.AI;
using Jiwar.Services.AI.Enums;
using Jiwar.Services.AI.Prompts.Renovations;
using System.Text.Json;

public class RenovationSimulationService : IRenovationSimulationService
{
    private readonly IRenovationSimulationRepository _repo;
    private readonly IAiService _aiService;

    public RenovationSimulationService(IRenovationSimulationRepository repo, IAiService aiService)
    {
        _repo = repo;
        _aiService = aiService;
    }

    // 1️⃣ Start
    public async Task<int> StartSimulationAsync(string userId, int propertyId)
    {
        var draft = await _repo.GetDraftByUserAsync(userId);
        if (draft != null)
            return draft.Id;

        var simulation = new RenovationSimulation
        {
            UserID = userId,
            PropertyID = propertyId,
            Status = SimulationStatusEnum.Draft,
            RenovationGoalsJson = "[]", // initialize to empty JSON array
        };

        await _repo.AddAsync(simulation);
        await _repo.SaveChangesAsync();

        return simulation.Id;
    }

    // 2️⃣ Details
    public async Task UpdateDetailsAsync(
        int simulationId,
        decimal size,
        int rooms,
        int bathrooms,
        string condition)
    {
        var simulation = await _repo.GetByIdAsync(simulationId)
            ?? throw new Exception("Simulation not found");

        // (لو حابة تربطي ده بجدول Property)
        // simulation.Property.Size = size;
        // ...

        await _repo.UpdateAsync(simulation);
        await _repo.SaveChangesAsync();
    }

    // 3️⃣ Media
    public async Task UploadMediaAsync(
        int simulationId,
        SimulationMediaTypeEnum type,
        string fileUrl)
    {
        var media = new SimulationMedia
        {
            RenovationSimulationID = simulationId,
            MediaType = type,
            FileUrl = fileUrl
        };

        await _repo.AddMediaAsync(media);
        await _repo.SaveChangesAsync();
    }

    // 4️ Goals & Budget
    public async Task SetGoalsAndBudgetAsync(
        int simulationId,
        List<string> goals,
        decimal? budgetMin,
        decimal? budgetMax)
    {
        var simulation = await _repo.GetByIdAsync(simulationId)
            ?? throw new Exception("Simulation not found");

        simulation.RenovationGoalsJson = JsonSerializer.Serialize(goals);
        simulation.BudgetMin = budgetMin;
        simulation.BudgetMax = budgetMax;

        await _repo.UpdateAsync(simulation);
        await _repo.SaveChangesAsync();
    }

    // 5️ Results
    public async Task CompleteSimulationAsync(int simulationId)
    {
        var simulation = await _repo.GetByIdAsync(simulationId)
            ?? throw new Exception("Simulation not found");

        simulation.Status = SimulationStatusEnum.Submitted;

        // 🔥 هنا مستقبلاً:
        // AI Recommendation Engine
        // ML Models
        // Cost Estimation

        await _repo.UpdateAsync(simulation);
        await _repo.SaveChangesAsync();
    }


    public async Task GenerateRecommendationsAsync(int simulationId)
    {
        var simulation = await _repo.GetByIdAsync(simulationId)
            ?? throw new Exception("Simulation not found");

        // 1️⃣ Build AI Context
        var context = RenovationContextBuilder.Build(simulation);

        // 2️⃣ Build Prompt
        var prompt = RenovationSystemPrompt.Build(context);

        // 3️⃣ Call AI
        var aiResponse = await _aiService.GenerateAsync(
            prompt,
            AiModelEnum.Gpt4o
        );

        // 4️⃣ Parse response
        var recommendations =
            RenovationRecommendationMapper.Map(aiResponse, simulationId);

        // 5️⃣ Save
        await _repo.AddRecommendationsAsync(recommendations);

        simulation.Status = SimulationStatusEnum.Analyzed;
        await _repo.SaveChangesAsync();
    }

    // 6️ Get Results
    //1-this is right
    //2-and i will try tomorow to complete it
    //3-and handlr ai recommendation engine
    //public Task<SimulationRecommendationDto> GetResultsAsync(int simulationId)
    //{

    //}
    //public async Task<RenovationSimulation?> GetResultsAsync(int simulationId)
    //{
    //    return await _repo.GetByIdAsync(simulationId);
    //}

    Task<SimulationRecommendationDto> IRenovationSimulationService.GetResultsAsync(int simulationId)
    {
        throw new NotImplementedException();
    }
}

