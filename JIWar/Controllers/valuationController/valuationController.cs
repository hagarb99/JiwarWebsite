using Jiwar.DTOs.ValuationDTOs;
using Jiwar.Services.ValuationService;
using Jiwar.DTOs.ValuationDTOs;

using Microsoft.AspNetCore.Mvc;

namespace Jiwar.Controllers.Valuation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuationController : ControllerBase
    {
        private readonly IValuationService _valuationService;
        private readonly IValuationHistoryService _historyService;

        public ValuationController(
            IValuationService valuationService,
            IValuationHistoryService historyService)
        {
            _valuationService = valuationService;
            _historyService = historyService;
        }

        [HttpPost("instant")]
        public IActionResult InstantValuation([FromBody] ValuationRequestDTO dto)
        {
            var result = _valuationService.CalculateValuation(dto);
            return Ok(result);
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveValuation([FromBody] ValuationSaveDTO dto)
        {
            await _historyService.SaveValuationAsync(dto);
            return Ok(new { message = "Valuation saved successfully" });
        }


        [HttpGet("my")]
        public async Task<IActionResult> GetMyValuations(string userId)
        {
            var list = await _historyService.GetMyValuationsAsync(userId);
            return Ok(list);
        }
    }
}
