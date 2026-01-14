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
        public async Task<IActionResult> SendProposal([FromBody] ProposalDto dto)
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

        [Authorize(Roles = "InteriorDesigner")]
        [HttpPost("deliver/{proposalId}")]
        public async Task<IActionResult> Deliver(int proposalId, [FromBody] DeliveryRequestDto dto)
        {
            var designerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {
                var result = await _service.DeliverProposalAsync(proposalId, designerId, dto.DeliveryNotes);
                return Ok(new { success = true, message = "Project marked as delivered" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    public class DeliveryRequestDto
    {
        public string DeliveryNotes { get; set; }
    }
}
