using Jiwar.Services.DesignerService;
using Microsoft.AspNetCore.Mvc;

namespace Jiwar.Controllers.DesignerController
{
    [ApiController]
    [Route("api/designer")]
    public class DesignerController : ControllerBase
    {
        private readonly IDesignerService _designerService;

        public DesignerController(IDesignerService designerService)
        {
            _designerService = designerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(string id)
        {
            var profile = await _designerService.GetDesignerProfileAsync(id);
            return Ok(profile);
        }
    }

}
