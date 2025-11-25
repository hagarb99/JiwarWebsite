using Jiwar.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jiwar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService reportService;
        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }
        //Get all reports for a user
        [HttpGet("my/{userId}")]
        public async Task<IActionResult> MyReports(string userId)
        {
            var reports = await reportService.GetUserReportsAsync(userId);
            return Ok(reports);
        }

        // Download a specific report
        [HttpGet("download/{id}")]
        public async Task<IActionResult> Download(int id)
        {
            var report = await reportService.GetReportByIdAsync(id);
            if (report == null || report.IsDeleted)
                return NotFound("Report not found");

            var filePath = $"wwwroot/reports/{id}.pdf";
            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found");

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "application/pdf", $"report_{id}.pdf");
        }


    }
}
