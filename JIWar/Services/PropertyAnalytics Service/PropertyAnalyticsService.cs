using System.Net.Http.Json;
using GEWAR;
using GEWAR.Models;
using Jiwar.Repositories;
using Jiwar.Services;
using Newtonsoft.Json;

public class PropertyAnalyticsService : IPropertyAnalyticsService
{
    private readonly IPropertyRepository _propertyRepo;
    private readonly GiwarContext _context;

    public PropertyAnalyticsService(IPropertyRepository propertyRepo , GiwarContext  context)
    {
        _propertyRepo = propertyRepo;
        _context = context;
    }

    public async Task<PropertyAnalytics> AnalyzePropertyAsync(Property property)
    {
        var comparables = await _propertyRepo.GetComparablePropertiesAsync(
            property.City,
            property.Area_sqm ?? 0,
            property.PropertyType.ToString(),
            areaTolerancePercentage: 15,
            ageToleranceYears: 10,
            minComps: 3
        );

        decimal estimatedPrice = comparables.Any()
            ? comparables.Average(p => p.Price)
            : await _propertyRepo.GetCityAveragePricePerSqmAsync(property.City) * (property.Area_sqm ?? 0);

        var analytics = new PropertyAnalytics
        {
            PropertyID = property.PropertyID,
            FairValue_Estimate = estimatedPrice,
            Price_Influence_Factors = JsonContent.SerializeObject(new
            {
                comparablesCount = comparables.Count(),
                usedFallback = !comparables.Any(),
                city = property.City,
                area = property.Area_sqm
            }),
            AnalysisDate = DateTime.UtcNow
        };

        _context.PropertyAnalytics.Add(analytics);

        property.EstimatedPrice = estimatedPrice; // Optional: لتسهيل العرض

        await _context.SaveChangesAsync();

        return analytics;
    }
}
