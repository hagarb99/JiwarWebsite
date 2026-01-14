using GEWAR.Models;

namespace Jiwar.DTOs
{
    public class AdminAnalyticsDTO
    {
        public UsersMetricsDTO UsersMetrics { get; set; }
        public PropertyMetricsDTO PropertyMetrics { get; set; }
        public ValuationMetricsDTO ValuationMetrics { get; set; }
        public PaymentMetricsDTO PaymentMetrics { get; set; }
        public EngagementMetricsDTO EngagementMetrics { get; set; }

      
    }

    public class UsersMetricsDTO
    {
        public int TotalUsers { get; set; }
        public int NewSignUpsToday { get; set; }
        public int NewSignUpsWeek { get; set; }
        public int NewSignUpsMonth { get; set; }
        public int ActiveUsers { get; set; }
        public Dictionary<string, int> UserRolesDistribution { get; set; } // Owner, Visitor, Agent
    }

    public class PropertyMetricsDTO
    {
        public int TotalProperties { get; set; }
        public int ActiveListings { get; set; }
        public int PendingListings { get; set; }
      
        public int ForSaleListings { get; set; }
        public int ForRentListings { get; set; }

        public List<TopCategoryDTO> TopCategories { get; set; }
        public List<TopDistrictDTO> TopDistricts { get; set; }
    }

    public class ValuationMetricsDTO
    {
        public int TotalValuations { get; set; }
        public Dictionary<string, int> ValuationsPerPeriod { get; set; } // day/week/month
    }

    public class PaymentMetricsDTO
    {
        public decimal TotalRevenue { get; set; }
        public Dictionary<string, decimal> RevenuePerPeriod { get; set; } // day/week/month
        public Dictionary<string, int> PaymentMethodDistribution { get; set; } // Paymob/Fawry

        // 📅 Today / Week / Month
        public Dictionary<string, decimal> RevenueByPeriod { get; set; }

        // 💳 Booking / Subscription / Report
        public Dictionary<string, decimal> RevenueByType { get; set; }

        public int TotalTransactions { get; set; }


        public PaymentMetricsDTO()
        {
            RevenueByPeriod = new Dictionary<string, decimal>();
            RevenueByType = new Dictionary<string, decimal>();
        }
    }

    public class EngagementMetricsDTO
    {
        public int PageVisits { get; set; }
        public int PropertyViews { get; set; }
        public Dictionary<string, int> SearchTrends { get; set; } // city/district
    }

    public class TopCategoryDTO
    {
       // public string CategoryName { get; set; }
  
     
        public string PropertyType { get; set; }
        public int Count { get; set; }
    }

    public class TopDistrictDTO
    {
        public string DistrictName { get; set; }
        public int Count { get; set; }
    }

 

}
