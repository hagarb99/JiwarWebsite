using Jiwar.Services.Interfaces;
using Jiwar.Repositories.Interfaces;
using Jiwar.Models;
using JIWar.PropertyOwner;
using Microsoft.EntityFrameworkCore;

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
            var list = await _subscriptionRepository.GetAllSubscriptionsAsync();

            return list.Select(s => new SubscriptionDetailsDTO
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price,
                DurationInMonths = s.
            });
        }

        public async Task<SubscriptionDetailsDTO> GetByIdAsync(int id)
        {
            var s = await _subscriptionRepository.GetSubscriptionByIdAsync(id);
            if (s == null) return null;

            return new SubscriptionDetailsDTO
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price
                DurationInMonths = s.D
            };
        }

        public async Task<SubscriptionDetailsDTO> CreateAsync(SubscriptionCreateDTO dto)
        {
            var model = new Subscription
            {
                Name = dto.Name,
                Price = dto.Price,
                DurationInMonths = dto.DurationInMonths
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
            var model = await _subscriptionRepository.GetSubscriptionByIdAsync(dto.Id);
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
            var model = await _subscriptionRepository.GetSubscriptionByIdAsync(id);
            if (model == null) return false;

            _subscriptionRepository.Remove(model);
            await _subscriptionRepository.SaveAsync();

            return true;
        }
    }
}
