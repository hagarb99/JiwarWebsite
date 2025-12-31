using GEWAR;
using GEWAR.Models;
using Google;
using Jiwar.DTOs.DesignDto;
using Jiwar.Models;
using Jiwar.Services.DesignerProposalService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Jiwar.Controllers.DesignerProposalController
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DesignerProposalController : ControllerBase
    {
        private readonly IDesignerProposalService _service;

        public DesignerProposalController(IDesignerProposalService service)
        {
            _service = service;
        }


        [Authorize(Roles = "InteriorDesigner")]

        [HttpPost("send")]
        public async Task<IActionResult> SendProposal([FromBody] DesignerProposalDto dto)
        {
            var designerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _service.SendProposalAsync(designerId, dto);
            return Ok(result);
        }

        [Authorize(Roles = "PropertyOwner,Customer")]
        [HttpGet("request/{requestId}")]
        public async Task<IActionResult> GetProposalsForRequest(int requestId)
        {
            var result = await _service.GetProposalsForRequestAsync(requestId);
            return Ok(result);
        }

        [Authorize(Roles = "InteriorDesigner")]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyProposals()
        {
            var designerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _service.GetProposalsForDesignerAsync(designerId);
            return Ok(result);
        }

        [Authorize(Roles = "PropertyOwner,Customer")]
        [HttpPost("choose/{proposalId}")]
        public async Task<IActionResult> ChooseProposal(int proposalId)
        {
            var ownerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _service.ChooseProposalAsync(proposalId, ownerId);
            return Ok(result);
        }

        [Authorize(Roles = "PropertyOwner,Customer")]
        [HttpGet("request/{requestId}/proposals")]
        public async Task<IActionResult> GetProposalsForOwner(int requestId)
        {
            var result = await _service.GetProposalsForRequestAsync(requestId);
            return Ok(result);
        }

    }

}
