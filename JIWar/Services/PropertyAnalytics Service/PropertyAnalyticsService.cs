using System.Net.Http.Json;
//using GEWAR;
//using GEWAR.Models;
//using Jiwar.DTOs.DistrictPriceHistoryDTOs;
//using Jiwar.Repositories;
//using Jiwar.Services;
//using Newtonsoft.Json;

//public class PropertyAnalyticsService : IPropertyAnalyticsService
//{
//    private readonly IPropertyRepository _propertyRepo;
//    private readonly GiwarContext _context;

//    public PropertyAnalyticsService(IPropertyRepository propertyRepo , GiwarContext  context)
//    {
//        _propertyRepo = propertyRepo;
//        _context = context;
//    }

//    public async Task<PropertyAnalytics> AnalyzePropertyAsync(Property property)
//    {
//        var comparables = await _propertyRepo.GetComparablePropertiesAsync(
//    property.City,
//    property.Area_sqm ?? 0,
//    property.PropertyType.ToString(),
//    areaTolerancePercentage: 15,
//    ageToleranceYears: 10,
//    minComps: 3
//);

//        decimal estimatedPrice = comparables.Any()
//            ? comparables.Average(p => p.Price)
//            : await _propertyRepo.GetCityAveragePricePerSqmAsync(property.City) * (property.Area_sqm ?? 0);

//        //var analytics = new PropertyAnalytics
//        //{
//        //    PropertyID = property.PropertyID,
//        //    FairValue_Estimate = estimatedPrice,
//        //    Price_Influence_Factors = JsonConvert.SerializeObject(new
//        //    {
//        //        comparablesCount = comparables.Count(),
//        //        usedFallback = !comparables.Any(),
//        //        city = property.City,
//        //        area = property.Area_sqm
//        //    }),
//        //    AnalysisDate = DateTime.UtcNow
//        //};

//        var analytics = new PropertyAnalytics
//        {
//            PropertyID = property.PropertyID,
//            FairValue_Estimate = estimatedPrice,
//            Price_Influence_Factors = JsonConvert.SerializeObject(new
//            {
//                comparablesCount = comparables.Count(),
//                usedFallback = !comparables.Any(),
//                city = property.City,
//                area = property.Area_sqm
//            }),
//            AnalysisDate = DateTime.UtcNow
//        };

//        _context.PropertyAnalytics.Add(analytics);
//        await _context.SaveChangesAsync();




//        _context.PropertyAnalytics.Add(analytics);

//        property.EstimatedPrice = estimatedPrice; 

//        await _context.SaveChangesAsync();

//        return analytics;
//    }

//    public Task<DistrictPriceHistoryDTO> GetDistrictPriceAnalytics(string district)
//    {
//        throw new NotImplementedException();
//    }
//}

using GEWAR;
using GEWAR.Models;
using Jiwar.DTOs.DistrictPriceHistoryDTOs;
using Jiwar.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Jiwar.Services
{
    public class PropertyAnalyticsService : IPropertyAnalyticsService
    {
        private readonly IPropertyRepository _propertyRepo;
        private readonly GiwarContext _context;

        public PropertyAnalyticsService(IPropertyRepository propertyRepo, GiwarContext context)
        {
            _propertyRepo = propertyRepo;
            _context = context;
        }

        public async Task<PropertyAnalytics> AnalyzePropertyAsync(Property property)
        {
            // ?????? ??? ???????? ???????? ????????
            var comparables = await _propertyRepo.GetComparablePropertiesAsync(
                property.City,
                property.Area_sqm ?? 0,
                property.PropertyType.ToString(),
                areaTolerancePercentage: 15,
                ageToleranceYears: 10,
                minComps: 3
            );

            // ???? ????? ????????
            decimal estimatedPrice = comparables.Any()
                ? comparables.Average(p => p.Price)
                : await _propertyRepo.GetCityAveragePricePerSqmAsync(property.City) * (property.Area_sqm ?? 0);

            // ????? ???? PropertyAnalytics ???? ????? ??? Id
            var analytics = new PropertyAnalytics
            {
                PropertyID = property.PropertyID,  // ??? ??? FK ????
                FairValue_Estimate = estimatedPrice,
                Price_Influence_Factors = JsonConvert.SerializeObject(new
                {
                    comparablesCount = comparables.Count(),
                    usedFallback = !comparables.Any(),
                    city = property.City,
                    area = property.Area_sqm
                }),
                AnalysisDate = DateTime.UtcNow
            };

            // ????? ??????? ????? ?? ????? ????????
            _context.PropertyAnalytics.Add(analytics);

            // ????? ????? ???????? ?? ??? Property ????
            property.EstimatedPrice = estimatedPrice;

            await _context.SaveChangesAsync();

            return analytics;
        }

        public Task<DistrictPriceHistoryDTO> GetDistrictPriceAnalytics(string district)
        {
            throw new NotImplementedException();
        }
    }
}



