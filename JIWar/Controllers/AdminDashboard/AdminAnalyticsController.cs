using Jiwar.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Jiwar.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/analytics")]
    [Authorize(Roles = "Admin")]
    public class AdminAnalyticsController : ControllerBase
    {
        private readonly IAdminAnalyticsService _analyticsService;

        public AdminAnalyticsController(IAdminAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet("admin/all")]
        public async Task<IActionResult> GetAnalytics()
        {
            var data = await _analyticsService.GetAnalyticsAsync();
            return Ok(data);
        }
    }
}
