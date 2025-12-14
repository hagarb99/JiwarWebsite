using GEWAR;
using GEWAR.Models;
using Jiwar.DTOs;
using Jiwar.Enum;
using Microsoft.EntityFrameworkCore;
using Jiwar.Repositories.Interfaces;

namespace Jiwar.Services
{
 
    public class AdminAnalyticsService : IAdminAnalyticsService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPropertyRepository _propertyRepo;
        private readonly IValuationRepository _valuationRepo;
        private readonly IPaymentRepository _paymentRepo;

        public AdminAnalyticsService(
            IUserRepository userRepo,
            IPropertyRepository propertyRepo,
            IValuationRepository valuationRepo,
            IPaymentRepository paymentRepo)
        {
            _userRepo = userRepo;
            _propertyRepo = propertyRepo;
            _valuationRepo = valuationRepo;
            _paymentRepo = paymentRepo;
        }

        public async Task<AdminAnalyticsDTO> GetAnalyticsAsync()
        {
            var users = await _userRepo.GetAllAsync();
            var properties = await _propertyRepo.GetAllAsync();
            var valuations = await _valuationRepo.GetAllAsync();
            var payments = await _paymentRepo.GetAllAsync();

            var dto = new AdminAnalyticsDTO
            {
                UsersMetrics = new UsersMetricsDTO
                {
                    TotalUsers = users.Count(),
                    NewSignUpsToday = users.Count(u => u.CreatedDate.Date == DateTime.Today),
                    NewSignUpsWeek = users.Count(u => u.CreatedDate >= DateTime.Today.AddDays(-7)),
                    NewSignUpsMonth = users.Count(u => u.CreatedDate >= DateTime.Today.AddMonths(-1)),
                    ActiveUsers = users.Count(u => u.LastLoginDate >= DateTime.Today.AddDays(-7)),
                    UserRolesDistribution = users
                        .GroupBy(u => u.Role)
                        .ToDictionary(g => g.Key.ToString(), g => g.Count())
                },

                PropertyMetrics = new PropertyMetricsDTO
                {
                    TotalProperties = properties.Count(),
                    ActiveListings = properties.Count(p => p.statusEnum == PropEnum.Active),
                    PendingListings = properties.Count(p => p.statusEnum == PropEnum.Pending),
                    SoldOrRentedUnits = properties.Count(p => p.statusEnum == PropEnum.Sold || p.statusEnum == PropEnum.Rented),
                    TopCategories = properties
                        .GroupBy(p => p.PropertyType.ToString())
                        .Select(g => new TopCategoryDTO { CategoryName = g.Key, Count = g.Count() })
                        .OrderByDescending(x => x.Count)
                        .Take(5)
                        .ToList(),
                    TopDistricts = properties
                        .GroupBy(p => p.District)
                        .Select(g => new TopDistrictDTO { DistrictName = g.Key, Count = g.Count() })
                        .OrderByDescending(x => x.Count)
                        .Take(5)
                        .ToList()
                },

                ValuationMetrics = new ValuationMetricsDTO
                {
                    TotalValuations = valuations.Count(),
                    ValuationsPerPeriod = new Dictionary<string, int>
                {
                    {"Today", valuations.Count(v => v.DateCreated.Date == DateTime.Today)},
                    {"Week", valuations.Count(v => v.DateCreated >= DateTime.Today.AddDays(-7))},
                    {"Month", valuations.Count(v => v.DateCreated >= DateTime.Today.AddMonths(-1))}
                }
                },

                PaymentMetrics = new PaymentMetricsDTO
                {
                    TotalRevenue = payments.Sum(p => p.Amount),
                    RevenuePerPeriod = new Dictionary<string, decimal>
                {
                    {"Today", payments.Where(p => p.PaymentDate.Date == DateTime.Today).Sum(p => p.Amount)},
                    {"Week", payments.Where(p => p.PaymentDate >= DateTime.Today.AddDays(-7)).Sum(p => p.Amount)},
                    {"Month", payments.Where(p => p.PaymentDate >= DateTime.Today.AddMonths(-1)).Sum(p => p.Amount)}
                },
                    PaymentMethodDistribution = payments
                        .GroupBy(p => p.PaymentMethod)
                        .ToDictionary(g => g.Key, g => g.Count())
                },

                EngagementMetrics = new EngagementMetricsDTO
                {
                    PageVisits = 0, // هنا لو عندك Google Analytics أو PageViews Table
                    PropertyViews = 0, // نفس الكلام
                    SearchTrends = new Dictionary<string, int>() // لو عندك Table للبحث
                }
            };

            return dto;
        }
    }
}
