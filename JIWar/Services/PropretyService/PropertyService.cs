using GEWAR.Models;
using Jiwar.DTOs.PropertyDTOs;
using Jiwar.Enum;
using Jiwar.Models;
using Jiwar.Repositories;
using Jiwar.Repositories.Interfaces;
namespace Jiwar.Service
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepo;

        public PropertyService(IPropertyRepository propertyRepo)
        {
            _propertyRepo = propertyRepo;
        }

        // 1. Add Property
        public async Task<Property> AddPropertyAsync(Property property)
        {
            await _propertyRepo.AddAsync(property);
            return property;
        }

        // 2. Update Property
        public async Task<bool> UpdatePropertyAsync(Property property)
        {
            var existing = await _propertyRepo.GetByIdAsync(property.PropertyID);
            if (existing == null) return false;

            _propertyRepo.Update(property);
            return true;
        }

        // 3. Delete Property (Soft Delete)
        public async Task<bool> DeletePropertyAsync(int id)
        {
            var property = await _propertyRepo.GetByIdAsync(id);
            if (property == null) return false;

            property.IsDeleted = true;
            _propertyRepo.Update(property);

            return true;
        }

        // 4. My Properties
        public Task<IEnumerable<Property>> GetMyPropertiesAsync(int ownerId)
        {
            return _propertyRepo.GetMyPropertiesAsync(ownerId);
        }

        // 5. Property Details
        public Task<Property> GetPropertyDetailsAsync(int id)
        {
            return _propertyRepo.GetPropertyDetailsAsync(id);
        }


        public async Task AddPropertyMediaAsync(PropertyMedia media)
        {
            await _propertyRepo.AddPropertyMediaAsync(media);
        }

        public async Task RemovePropertyMediaAsync(int mediaId)
        {
            await _propertyRepo.RemovePropertyMediaAsync(mediaId);

        }

        public async Task UpdatePropertyStatusAsync(int id, PropEnum status)
        {
            await _propertyRepo.UpdatePropertyStatusAsync(id, status);

        }
        public async Task SendMessageAsync(Chat chat)
        {
            await _propertyRepo.SendMessageAsync(chat);
        }

        public async Task<IEnumerable<Chat>> GetChatHistoryAsync(string senderId, string receiverId, int propertyId)
        {
            return await _propertyRepo.GetChatHistoryAsync(senderId, receiverId, propertyId);
        }

        public async Task<IEnumerable<Property>> GetFilteredPropertiesAsync(PropertyFilterDTO filter)
        {
            return await _propertyRepo.GetFilteredPropertiesAsync(filter);
        }

        public async Task<IEnumerable<PropertyComparisonDTO>> GetPropertiesForComparisonAsync(List<int> propertyIds)
        {
            var properties = await _propertyRepo.GetPropertiesByIdsAsync(propertyIds);

            return properties.Select(p => new PropertyComparisonDTO
            {
                PropertyID = p.PropertyID,
                Title = p.Title,
                City = p.City,
                Address = p.Address,
                Price = p.Price,
                Area_sqm = p.Area_sqm,
                NumBedrooms = p.NumBedrooms,
                NumBathrooms = p.NumBathrooms,
                PropertyType = p.PropertyType.ToString(),
                Status = p.statusEnum.ToString(),
                ThumbnailUrl = p.PropertyMedia.FirstOrDefault()?.MediaURL,
                Features = p.PropertyFeatures?
            .Where(pf => pf.Feature != null)
            .Select(pf => pf.Feature.Name)
            .ToList() ?? new List<string>()

            });
        }
    }
}
