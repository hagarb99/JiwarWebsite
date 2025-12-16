using Jiwar.Services.RequestService;
using Microsoft.AspNetCore.Mvc;

namespace Jiwar.Controllers
{
    [ApiController]
    [Route("api/requests")]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableRequests()
        {
            return Ok(await _requestService.GetAvailableRequestsAsync());
        }
    }

}
