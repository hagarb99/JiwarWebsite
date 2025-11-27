using GEWAR.Models;

namespace Jiwar.Services
{
    public interface IPropertyAnalyticsService
{
    Task<PropertyAnalytics> AnalyzePropertyAsync(Property property);
}

}