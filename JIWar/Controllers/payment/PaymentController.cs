using GEWAR.Models;
using Jiwar.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Jiwar.Controllers.payment
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("subscription/{planId}")]
        public async Task<IActionResult> CreateSubscriptionPayment(int planId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // ← هنا User متعرف
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(); // ← هنا Unauthorized متعرف

            var iframeUrl = await _paymentService.CreateSubscriptionPaymentAsync(userId, planId);
            return Ok(new { iframeUrl });
        }
    }
}
