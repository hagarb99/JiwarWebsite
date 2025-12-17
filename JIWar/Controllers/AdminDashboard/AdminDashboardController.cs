using Jiwar.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jiwar.Controllers
{
[Route("api/[controller]")]
[ApiController]
public class AdminAnalyticsController : ControllerBase
{
    private readonly IAdminAnalyticsService _analyticsService;

    public AdminAnalyticsController(IAdminAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard()
    {
        var result = await _analyticsService.GetDashboardDataAsync();
        return Ok(result);
    }
}

}
