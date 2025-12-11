using GEWAR.Models;
using Jiwar.Services.DesignService;
using Microsoft.AspNetCore.Mvc;

namespace Jiwar.Controllers.DesignController
{
    [ApiController]
    [Route("api/designs")]
    public class DesignController : ControllerBase
    {
        private readonly IDesignService _designService;

        public DesignController(IDesignService designService)
        {
            _designService = designService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadDesign(Design design)
        {
            return Ok(await _designService.UploadDesignAsync(design));
        }
    }

}
