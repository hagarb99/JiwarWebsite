using GEWAR.Models;
using Jiwar.DTOs;
using Jiwar.Enum;
using Jiwar.Repositories;
using Jiwar.Repositories.Interfaces;
using Jiwar.Repositories;
using Jiwar.Repositories.Valuation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jiwar.DTOs.Payment;

namespace Jiwar.Services
{
    public class AdminAnalyticsService : IAdminAnalyticsService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPropertyRepository _propertyRepo;
        private readonly IValuationHistoryRepository _valuationRepo;
        private readonly IBookingPaymentRepository _bookingPaymentRepo;
        private readonly ISubscriptionRepository _subscriptionRepo;
        private readonly IReportOrderRepository _reportOrderRepo;

        public AdminAnalyticsService(
            IUserRepository userRepo,
            IPropertyRepository propertyRepo,
            IValuationHistoryRepository valuationRepo,
            IBookingPaymentRepository bookingPaymentRepo,
            ISubscriptionRepository subscriptionRepo,
            IReportOrderRepository reportOrderRepo)
        {
            _userRepo = userRepo;
            _propertyRepo = propertyRepo;
            _valuationRepo = valuationRepo;
            _bookingPaymentRepo = bookingPaymentRepo;
            _subscriptionRepo = subscriptionRepo;
            _reportOrderRepo = reportOrderRepo;
        }

        public async Task<AdminAnalyticsDTO> GetAnalyticsAsync()
        {
            // Users Metrics
            var usersMetrics = new UsersMetricsDTO
            {
                TotalUsers = await _userRepo.GetTotalUsersAsync(),
                NewSignUpsToday = await _userRepo.GetNewUsersTodayAsync(),
                NewSignUpsWeek = await _userRepo.GetNewUsersThisWeekAsync(),
                NewSignUpsMonth = await _userRepo.GetNewUsersThisMonthAsync(),
                ActiveUsers = await _userRepo.GetActiveUsersAsync(),
                UserRolesDistribution = await _userRepo.GetUsersCountByRoleAsync()
            };

            // Property Metrics
            var properties = await _propertyRepo.GetAllAsync();
            var propertyMetrics = new PropertyMetricsDTO
            {
                TotalProperties = properties.Count(),
                ActiveListings = properties.Count(p => p.statusEnum == PropEnum.Active),
                SoldOrRentedUnits = properties.Count(p =>
                    p.statusEnum == PropEnum.Sold || p.statusEnum == PropEnum.Rented),
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
            };

            // Valuation Metrics
            var valuationMetrics = new ValuationMetricsDTO
            {
                TotalValuations = await _valuationRepo.GetTotalValuationsAsync(),
                ValuationsPerPeriod = new Dictionary<string, int>
                {
                    { "Today", await _valuationRepo.GetValuationsTodayAsync() },
                    { "Week", await _valuationRepo.GetValuationsThisWeekAsync() },
                    { "Month", await _valuationRepo.GetValuationsThisMonthAsync() }
                }
            };


          


            // Payment Metrics
            var bookingRevenue = await _bookingPaymentRepo.GetTotalRevenueAsync();
            var subscriptionRevenue = await _subscriptionRepo.GetTotalRevenueAsync();
            var reportRevenue = await _reportOrderRepo.GetTotalRevenueAsync();

            var paymentMetrics = new Jiwar.DTOs.PaymentMetricsDTO // نستخدم النوع الصحيح هنا
            {
                TotalRevenue = bookingRevenue + subscriptionRevenue + reportRevenue,
                RevenueByType = new Dictionary<string, decimal>
                {
                    { "Booking", bookingRevenue },
                    { "Subscription", subscriptionRevenue },
                    { "Report", reportRevenue }
                },
                RevenueByPeriod = new Dictionary<string, decimal>
                {
                    { "Today", bookingRevenue + subscriptionRevenue + reportRevenue },
                    { "Week", bookingRevenue + subscriptionRevenue + reportRevenue },
                    { "Month", bookingRevenue + subscriptionRevenue + reportRevenue }
                }
            };

            // Admin Analytics DTO
            return new AdminAnalyticsDTO
            {
                UsersMetrics = usersMetrics,
                PropertyMetrics = propertyMetrics,
                ValuationMetrics = valuationMetrics,
                PaymentMetrics = paymentMetrics,
                EngagementMetrics = new EngagementMetricsDTO
                {
                    PageVisits = 0,
                    PropertyViews = 0,
                    SearchTrends = new Dictionary<string, int>()
                }
            };

      

    }
}
}
