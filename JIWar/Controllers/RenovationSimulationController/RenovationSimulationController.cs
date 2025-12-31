using GEWAR;
using GEWAR.Models;
using GEWAR.Models.Jiwar.Enum;
using Jiwar.DTOs;
using Jiwar.Enum;
using Jiwar.Models;
using Jiwar.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[ApiController]
[Route("api/renovation-simulations")]
[Authorize]
public class RenovationSimulationsController : ControllerBase
{
    private readonly IRenovationSimulationService _service;

    public RenovationSimulationsController(
        IRenovationSimulationService service)
    {
        _service = service;
    }

    private string UserId =>
        User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    // 1️⃣ Start Simulation
    [HttpPost("start")]
    public async Task<ActionResult<int>> Start(
        [FromBody] StartSimulationDto dto)
    {
        var id = await _service.StartSimulationAsync(dto, UserId);
        return Ok(id);
    }

    // 2️⃣ Update Details
    [HttpPut("{id:int}/details")]
    public async Task<IActionResult> UpdateDetails(
        int id,
        [FromBody] UpdateSimulationDetailsDto dto)
    {
        await _service.UpdateDetailsAsync(id, dto);
        return NoContent();
    }

    // 3️⃣ Upload Media
    [HttpPost("{id:int}/media")]
    public async Task<IActionResult> UploadMedia(
        int id,
        [FromBody] UploadSimulationMediaDto dto)
    {
        await _service.UploadMediaAsync(id, dto);
        return NoContent();
    }

    // 4️⃣ Goals & Budget
    [HttpPut("{id:int}/goals")]
    public async Task<IActionResult> SetGoals(
        int id,
        [FromBody] SimulationGoalsDto dto)
    {
        await _service.SetGoalsAndBudgetAsync(id, dto);
        return NoContent();
    }

    // 5️⃣ Submit Simulation
    [HttpPost("{id:int}/submit")]
    public async Task<IActionResult> Submit(int id)
    {
        await _service.CompleteSimulationAsync(id);
        return NoContent();
    }

    // 6️⃣ AI Analyze
    [HttpPost("{id:int}/analyze")]
    public async Task<IActionResult> Analyze(int id)
    {
        await _service.GenerateRecommendationsAsync(id);
        return Accepted(); // AI async process
    }

    // 7️⃣ Get Results
    [HttpGet("{id:int}/results")]
    public async Task<ActionResult<SimulationResultDto>> Results(int id)
    {
        var result = await _service.GetResultsAsync(id);
        return Ok(result);
    }
}

