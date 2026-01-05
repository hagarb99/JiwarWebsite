using GEWAR.Models;
using Jiwar.Models;
using Jiwar.Services.ProposalService;
using Microsoft.AspNetCore.Mvc;

namespace Jiwar.Controllers.ProposalController
{
    [ApiController]
    [Route("api/proposals")]
    public class ProposalController : ControllerBase
    {
        private readonly IProposalService _proposalService;

        public ProposalController(IProposalService proposalService)
        {
            _proposalService = proposalService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(DesignerProposal proposal)
        {
            return Ok(await _proposalService.CreateProposalAsync(proposal));
        }
    }

}
