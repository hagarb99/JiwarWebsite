using GEWAR;
using Jiwar.DTOs;
using Microsoft.EntityFrameworkCore;
using GEWAR.Models;

namespace Jiwar.Services
{
    public class AdminAnalyticsService : IAdminAnalyticsService
{
    private readonly GiwarContext  _context;

    public AdminAnalyticsService(GiwarContext context)
    {
        _context = context;
    }

    public async Task<AdminAnalyticsDTO> GetDashboardDataAsync()
    {
        var dto = new AdminAnalyticsDTO();

        dto.TotalValuations = await _context.PropertyAnalytics.CountAsync();

        dto.TotalUsers = await _context.Users.CountAsync();

        dto.TopDistricts = await _context.Properties
            .GroupBy(p => p.District)
            .Select(g => new TopDistrictDTO
            {
                District = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToListAsync();

        return dto;
    }
}

}