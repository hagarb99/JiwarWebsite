using Jiwar.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Jiwar.Controllers
{
    [ApiController]
    [Route("api/admin/analytics")]
  

    public class AdminAnalyticsController : ControllerBase
    {
        private readonly IAdminAnalyticsService _analyticsService;

        public AdminAnalyticsController(IAdminAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnalytics()
        {
            var data = await _analyticsService.GetAnalyticsAsync();
            return Ok(data);
        }
    }
}
