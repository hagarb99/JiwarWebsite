using Jiwar.DTOs;
using Jiwar.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Jiwar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService reportService;
        private readonly IPaymentService _paymentService;
        public ReportController(IReportService reportService, IPaymentService paymentService)
        {
            this.reportService = reportService;
            _paymentService = paymentService;
        }
        //Get all reports for a user
        [HttpGet("my/{userId}")]
        public async Task<IActionResult> MyReports(string userId)
        {
            var reports = await reportService.GetUserReportsAsync(userId);
            return Ok(reports);
        }

        // Download a specific report
        [Authorize]
        [HttpGet("download/{id}")]
        public async Task<IActionResult> Download(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var hasPaid = await _paymentService.HasUserPaidForReportAsync(userId, id);

            if (!hasPaid)
                return Forbid("You must purchase this report before downloading.");

            var filePath = $"wwwroot/reports/{id}.pdf";
            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found");

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "application/pdf", $"report_{id}.pdf");
        }

        [HttpPost("buy")]
        public async Task<IActionResult> BuyReport([FromBody] BuyReportDto dto)
        {
            var paymentUrl = await _paymentService.CreateReportPaymentAsync(dto.UserId, dto.ReportId);
            return Ok(new { paymentUrl });
        }

        [HttpPost("payment/webhook")]
        public async Task<IActionResult> PaymentWebhook([FromBody] PaymobWebhookDto dto)
        {
            await _paymentService.HandlePaymobWebhookAsync(dto);
            return Ok("Payment confirmed");
        }

    }
}
