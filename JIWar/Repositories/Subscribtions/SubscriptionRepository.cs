using GEWAR;
using GEWAR.Models;
using Jiwar.Models; // Subscription model
using Jiwar.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jiwar.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly GiwarContext _context;

        public SubscriptionRepository(GiwarContext context)
        {
            _context = context;
        }

        // ===============================
        // CRUD Methods
        // ===============================
        public async Task<IEnumerable<Subscription>> GetAllAsync()
        {
            return await _context.Subscriptions.ToListAsync();
        }

        public async Task<Subscription?> GetByIdAsync(int id)
        {
            return await _context.Subscriptions.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
        }

        public void Update(Subscription subscription)
        {
            _context.Subscriptions.Update(subscription);
        }

        public void Remove(Subscription subscription)
        {
            _context.Subscriptions.Remove(subscription);
        }

        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // هتطبع الرسالة الحقيقية للخطأ اللي حصل
                Console.WriteLine(ex.InnerException?.Message);
                throw; // هيرمي الخطأ تاني عشان تعرفي في مكان الاستدعاء
            }
        }



        // ===============================
        // Revenue Methods
        // ===============================
        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.Subscriptions
                .Where(s => s.statusEnum2 == StatusEnum2.Active)
                .SumAsync(s => s.Price);
        }

        public async Task<decimal> GetTodayRevenueAsync()
        {
            var today = DateTime.UtcNow.Date;
            return await _context.Subscriptions
                .Where(s => s.statusEnum2 == StatusEnum2.Active && s.StartDate.Date == today)
                .SumAsync(s => s.Price);
        }

        public async Task<decimal> GetWeekRevenueAsync()
        {
            var lastWeek = DateTime.UtcNow.AddDays(-7);
            return await _context.Subscriptions
                .Where(s => s.statusEnum2 == StatusEnum2.Active && s.StartDate >= lastWeek)
                .SumAsync(s => s.Price);
        }

        public async Task<decimal> GetMonthRevenueAsync()
        {
            var startOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            return await _context.Subscriptions
                .Where(s => s.statusEnum2 == StatusEnum2.Active && s.StartDate >= startOfMonth)
                .SumAsync(s => s.Price);
        }
    }
}
