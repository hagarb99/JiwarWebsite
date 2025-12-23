using Jiwar.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/renovation-simulations")]
[Authorize]
public class RenovationSimulationController : ControllerBase
{
    private readonly IRenovationSimulationService _service;

    public RenovationSimulationController(IRenovationSimulationService service)
    {
        _service = service;
    }

    // 1️⃣ Start Simulation
    [HttpPost("start")]
    public async Task<IActionResult> Start([FromBody] StartSimulationDto dto)
    {
        //var userId = User.FindFirst("sub")?.Value
        //    ?? User.FindFirst("id")?.Value;

        //if (userId == null)
        //    return Unauthorized();
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var simulationId = await _service.StartSimulationAsync(userId, dto.PropertyId);

        return Ok(new { SimulationId = simulationId });
    }

    // 2️⃣ Update Details
    [HttpPut("{id}/details")]
    public async Task<IActionResult> UpdateDetails(
        int id,
        [FromBody] UpdateSimulationDetailsDto dto)
    {
        await _service.UpdateDetailsAsync(
            id,
            dto.Size,
            dto.Rooms,
            dto.Bathrooms,
            dto.Condition
        );

        return NoContent();
    }

    // 3️⃣ Upload Media
    [HttpPost("{id}/media")]
    public async Task<IActionResult> UploadMedia(
        int id,
        [FromBody] UploadSimulationMediaDto dto)
    {
        await _service.UploadMediaAsync(
            id,
            dto.MediaType,
            dto.FileUrl
        );

        return Ok();
    }

    // 4️⃣ Set Goals & Budget
    [HttpPut("{id}/goals")]
    public async Task<IActionResult> SetGoals(
        int id,
        [FromBody] SimulationGoalsDto dto)
    {
        await _service.SetGoalsAndBudgetAsync(
            id,
            dto.Goals,
            dto.BudgetMin,
            dto.BudgetMax
        );

        return NoContent();
    }

    // 5️⃣ Complete Simulation
    [HttpPost("{id}/complete")]
    public async Task<IActionResult> Complete(int id)
    {
        await _service.CompleteSimulationAsync(id);
        return Ok();
    }

    //  AI Generate Recommendations
    [HttpPost("{id}/generate-recommendations")]
    public async Task<IActionResult> GenerateRecommendations(int id)
    {
        await _service.GenerateRecommendationsAsync(id);
        return Ok();
    }



    // 6️⃣ Get Results
    [HttpGet("{id}")]
    public async Task<IActionResult> GetResults(int id)
    {
        var result = await _service.GetResultsAsync(id);
        return Ok(result);
    }

}

    //public async Task<IActionResult> GetResults(int id)
    //{
    //    var result = await _service.GetResultsAsync(id);

//    if (result == null)
//        return NotFound();

//    return Ok(result);
//}


