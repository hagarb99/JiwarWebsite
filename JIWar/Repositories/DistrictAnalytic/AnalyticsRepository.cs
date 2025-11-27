using GEWAR;
using Jiwar.Models;
using Jiwar.Services.DistrictAnalyticService;
using Microsoft.EntityFrameworkCore;

namespace Jiwar.Repositories.DistrictAnalyticService
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly GiwarContext _context;

        public AnalyticsRepository(GiwarContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DistrictPriceHistory>> GetDistrictHistoryAsync(string district, int years)
        {
            DateTime fromDate = DateTime.UtcNow.AddYears(-years);

            return await _context.DistrictPriceHistories
                .Where(x => x.District == district && x.RecordDate >= fromDate)
                .OrderBy(x => x.RecordDate)
                .ToListAsync();
        }

        public async Task<List<PropertyPriceHistory>> GetDistrictPriceHistoryAsync(string district)
        {
            return await _context.PropertyPriceHistories
                .Where(x => x.Property.City == district || x.Property.District== district) // حسب جدولك
                .OrderBy(x => x.DateRecorded)
                .ToListAsync();
        }

    }

}
