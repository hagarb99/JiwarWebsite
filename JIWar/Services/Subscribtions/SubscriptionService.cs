using GEWAR.Models;
using Jiwar.DTOs;
using Jiwar.Models;
using Jiwar.Repositories.Interfaces;
using JIWar.PropertyOwner;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jiwar.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<IEnumerable<SubscriptionDetailsDTO>> GetAllAsync()
        {
            var list = await _subscriptionRepository.GetAllAsync();

            return list.Select(s => new SubscriptionDetailsDTO
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price,
                DurationInMonths = s.DurationInMonths
            });
        }

        public async Task<SubscriptionDetailsDTO?> GetByIdAsync(int id)
        {
            var s = await _subscriptionRepository.GetByIdAsync(id);
            if (s == null) return null;

            return new SubscriptionDetailsDTO
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price,
                DurationInMonths = s.DurationInMonths
            };
        }




        public async Task<SubscriptionDetailsDTO> CreateAsync(SubscriptionCreateDTO dto, string userId)
        {

            var model = new Subscription
            {
                Name = dto.Name,
                Price = dto.Price,
                DurationInMonths = dto.DurationInMonths,
                StartDate = DateTime.UtcNow, // تاريخ البداية افتراضي
                EndDate = DateTime.UtcNow.AddMonths(dto.DurationInMonths),
                PlanType = dto.PlanType,
                planTypeEnum = PlanTypeEnum.Golden,          // أو حسب اختيارك
                Status = "Active",
                statusEnum2 = StatusEnum2.Active,
            

                UserID = userId
            };


            await _subscriptionRepository.AddAsync(model);
            await _subscriptionRepository.SaveAsync();

            return new SubscriptionDetailsDTO
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                DurationInMonths = model.DurationInMonths
            };
        }

        public async Task<bool> UpdateAsync(SubscriptionUpdateDTO dto)
        {
            var model = await _subscriptionRepository.GetByIdAsync(dto.Id);
            if (model == null) return false;

            model.Name = dto.Name;
            model.Price = dto.Price;
            model.DurationInMonths = dto.DurationInMonths;

            _subscriptionRepository.Update(model);
            await _subscriptionRepository.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var model = await _subscriptionRepository.GetByIdAsync(id);
            if (model == null) return false;

            _subscriptionRepository.Remove(model);
            await _subscriptionRepository.SaveAsync();

            return true;
        }
    }
}
