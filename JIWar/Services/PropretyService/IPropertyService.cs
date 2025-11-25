using GEWAR.Models;
using Jiwar.DTOs.PropertyDTOs;
using Jiwar.Enum;
using Jiwar.Models;

namespace Jiwar.Service
{
    public interface IPropertyService
    {
        // Add a new property
        public  Task<Property> AddPropertyAsync(Property property);

        // Update an existing property
       public Task<bool> UpdatePropertyAsync(Property property);

        // Soft delete property
       public Task<bool> DeletePropertyAsync(int id);

        // Get all properties of an owner
       public Task<IEnumerable<Property>> GetMyPropertiesAsync(int ownerId);

        // Get details of a specific property
       public Task<Property> GetPropertyDetailsAsync(int id);

        // Add media to a property
       public Task AddPropertyMediaAsync(PropertyMedia media);

        // Remove media from a property
       public Task RemovePropertyMediaAsync(int mediaId);

        // Update property status (Active / Inactive / Pending)
       public Task UpdatePropertyStatusAsync(int id, PropEnum status);
       public Task SendMessageAsync(Chat chat);
        public Task<IEnumerable<Chat>> GetChatHistoryAsync(string senderId, string receiverId, int propertyId);

        Task<IEnumerable<Property>> GetFilteredPropertiesAsync(PropertyFilterDTO filter);

        Task<IEnumerable<PropertyComparisonDTO>> GetPropertiesForComparisonAsync(List<int> propertyIds);
    }
}
