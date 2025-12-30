using GEWAR;
using GEWAR.Models;
using Jiwar.DTOs;
using Jiwar.Models;
using Jiwar.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/renovation-simulations")]
[Authorize]
public class RenovationSimulationsController : ControllerBase
{
    private readonly IRenovationSimulationService _service;
    private readonly GiwarContext _context;

    public RenovationSimulationsController(IRenovationSimulationService service)
    {
        _service = service;
    }

    private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    // 1️⃣ Start
    [HttpPost("start")]
    public async Task<int> StartSimulationAsync(string userId, int? propertyId, decimal budgetMin, decimal budgetMax, string goalsJson)
    {
        var simulation = new RenovationSimulation
        {
            UserID = userId,
            PropertyID = propertyId,
            BudgetMin = budgetMin,
            BudgetMax = budgetMax,
            RenovationGoalsJson = goalsJson
        };

        _context.RenovationSimulations.Add(simulation);
        await _context.SaveChangesAsync();

        return simulation.Id;
    }

    // 2️⃣ Details
    [HttpPut("{id:int}/details")]
    public async Task UpdateDetailsAsync(int simulationId, decimal size, int rooms, int bathrooms, string condition)
    {
        var simulation = await _context.RenovationSimulations.FindAsync(simulationId);

        if (simulation.PropertyID == null)
        {
            // Standalone simulation → save details in SimulationDetails
            var details = new SimulationDetails
            {
                RenovationSimulationID = simulationId,
                Size = size,
                Rooms = rooms,
                Bathrooms = bathrooms,
                Condition = condition
            };
            _context.SimulationDetails.Add(details);
            await _context.SaveChangesAsync();
        }
        else
        {
            // Optional: override Property data or ignore
        }
    }

        // 3️⃣ Media
     [HttpPost("{id:int}/media")]
    public async Task<IActionResult> UploadMedia(int id, [FromBody] UploadSimulationMediaDto request)
    {
        await _service.UploadMediaAsync(
            id,
            request.MediaType,
            request.FileUrl);

        return NoContent();
    }

    // 4️⃣ Goals & Budget
    [HttpPut("{id:int}/goals")]
    public async Task<IActionResult> SetGoals(int id, [FromBody] SimulationGoalsDto request)
    {
        await _service.SetGoalsAndBudgetAsync(
            id,
            request.Goals,
            request.BudgetMin,
            request.BudgetMax);

        return NoContent();
    }

    // 5️⃣ Submit
    [HttpPost("{id:int}/submit")]
    public async Task<IActionResult> Submit(int id)
    {
        await _service.CompleteSimulationAsync(id);
        return NoContent(); // Fixed missing parentheses
    }

    // 6️⃣ AI Analyze
    [HttpPost("{id:int}/analyze")]
    public async Task<IActionResult> Analyze(int id)
    {
        await _service.GenerateRecommendationsAsync(id);
        return Accepted(); // async AI job
    }

    // 7️⃣ Results
    [HttpGet("{id:int}/results")]
    public async Task<IActionResult> Results(int id)
    {
        var result = await _service.GetResultsAsync(id);
        return Ok(result);
    }
}
