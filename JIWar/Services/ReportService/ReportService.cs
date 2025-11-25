using GEWAR.Models;
using Jiwar.DTOs.PropertyDTOs;
using Jiwar.Repositories;

namespace Jiwar.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepo;

        public ReportService(IReportRepository reportRepo)
        {
            _reportRepo = reportRepo;
        }
        public async Task<IEnumerable<ReportDTO>> GetUserReportsAsync(string userId)
        {
            var reports = await _reportRepo.GetReportsByUserAsync(userId);

            return reports.Select(r => new ReportDTO
            {
                Id = r.Id,
                ReportType = r.ReportType,
                Description = r.Description,
                Status = r.Status,
                CreatedDate = r.CreatedDate,
                DownloadUrl = $"https://yourdomain.com/api/report/download/{r.Id}"
            });
        }

        public async Task<Report?> GetReportByIdAsync(int id)
        {
            return await _reportRepo.GetByIdAsync(id);
        }

    }
}
