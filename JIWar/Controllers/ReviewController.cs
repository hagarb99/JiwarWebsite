using Jiwar.DTOs.ReviewDTOs;
using Jiwar.Services.ReviewService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Jiwar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitReview([FromBody] CreateReviewDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != dto.PropertyOwnerId) return Unauthorized();

            try
            {
                await _reviewService.SubmitReviewAsync(dto);
                return Ok(new { success = true, message = "Review submitted successfully" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("designer/{designerId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDesignerReviews(string designerId)
        {
            var summary = await _reviewService.GetDesignerReviewsAsync(designerId);
            return Ok(summary);
        }
    }
}
