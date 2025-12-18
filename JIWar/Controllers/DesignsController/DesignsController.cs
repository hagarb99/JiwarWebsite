using GEWAR;
using GEWAR.Models;
using Google;
using Jiwar.DTOs.DesignDto;
using Jiwar.Models;
using Jiwar.Services.DesignService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Jiwar.Controllers.DesignsController
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DesignsController : ControllerBase
    {
        private readonly IDesignService _service;

        public DesignsController(IDesignService service)
        {
            _service = service;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadDesign([FromBody] CreateDesignDto dto)
        {
            var designerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _service.UploadFinalDesignAsync(designerId, dto);
            return Ok(result);
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyDesigns()
        {
            var designerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _service.GetDesignsByDesignerAsync(designerId);
            return Ok(result);
        }

        [HttpGet("owner")]
        public async Task<IActionResult> GetOwnerDesigns()
        {
            var ownerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _service.GetDesignsByOwnerAsync(ownerId);
            return Ok(result);
        }

        [HttpGet("property/{propertyId}")]
        public async Task<IActionResult> GetDesignsByProperty(int propertyId)
        {
            var result = await _service.GetDesignsByPropertyAsync(propertyId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDesignById(int id)
        {
            var result = await _service.GetDesignByIdAsync(id);
            return Ok(result);
        }
    }

}
