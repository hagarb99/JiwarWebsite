using Jiwar.DTOs.DesignDto;
using Jiwar.Services.DesignRequestService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Jiwar.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DesignRequestController : ControllerBase
    {
        private readonly IDesignRequestService _service;

        public DesignRequestController(IDesignRequestService service)
        {
            _service = service;
        }

        [Authorize(Roles = "PropertyOwner,Customer")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateDesignRequest([FromBody] DesignRequestDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var result = await _service.CreateDesignRequestAsync(userId, dto);
            return Ok(result);
        }

        [Authorize(Roles = "PropertyOwner,Customer")]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyRequests()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var result = await _service.GetUserRequestsAsync(userId);
            return Ok(result);
        }



        [Authorize(Roles = "InteriorDesigner")]
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableRequests()
        {
            var result = await _service.GetAvailableRequestsAsync();
            return Ok(result);
        }


        [Authorize(Roles = "PropertyOwner,Customer,InteriorDesigner")]

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequestById(int id)
        {
            var result = await _service.GetRequestByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }
    }
}
