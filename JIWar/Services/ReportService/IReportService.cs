using GEWAR.Models;
using Jiwar.DTOs.PropertyDTOs;

namespace Jiwar.Services
{
    public interface IReportService
    {
        Task<IEnumerable<ReportDTO>> GetUserReportsAsync(string userId);
        Task<Report?> GetReportByIdAsync(int id); 

    }
}
