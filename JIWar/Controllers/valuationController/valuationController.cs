using GEWAR.Models;
using Jiwar.DTOs.ValuationDTOs;
using Jiwar.DTOs.ValuationDTOs;
using Jiwar.Services.ValuationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> SaveValuation([FromBody] ValuationSaveDTO dto, string userId)
        {
           
           
            await _historyService.SaveValuationAsync(dto, userId);


            return Ok(new { message = "Valuation saved successfully" });
        }

       
[HttpPost("calculate")]
    [Authorize]
    public async Task<IActionResult> CalculateAndSave([FromBody] ValuationRequestDTO dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           
            if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var result = _valuationService.CalculateValuation(dto);

        await _historyService.SaveValuationAsync(new ValuationSaveDTO
        {
            UserId = userId,
            City = dto.City,
            Area = dto.Area,
            Bedrooms = dto.Bedrooms,
            Bathrooms = dto.Bathrooms,
            FinishType = dto.FinishType,
            View = dto.View,
            PropertyAge = dto.PropertyAge,
            MostLikelyPrice = result.MostLikelyPrice,
            MinPrice = result.MinPrice,
            MaxPrice = result.MaxPrice,
            ConfidenceScore = result.ConfidenceScore
        }, userId);

        return Ok(result);
    }
   

      

        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyValuations()
        {
            var userId = User.FindFirst("nameidentifier")?.Value;
            var list = await _historyService.GetMyValuationsAsync(userId);
            return Ok(list);
        }

    }
}
