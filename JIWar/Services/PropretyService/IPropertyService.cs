using GEWAR.Models;
using Jiwar.DTOs;
using Jiwar.DTOs.PropertyDTOs;
using Jiwar.Enum;
using Jiwar.Models;
using JIWar.PropertyOwner;

namespace Jiwar.Service
{
    public interface IPropertyService
    {
        // Add a new property
        //public  Task<Property> AddPropertyAsync(Property property);
        Task<PropertyWithAnalyticsDTO> AddPropertyAsync(PropertyCreateDTO dto, string ownerId);

       public Task<bool> UpdatePropertyAsync(Property property);

       public Task<bool> DeletePropertyAsync(int id);

        // Get all properties of an owner
       public Task<IEnumerable<Property>> GetMyPropertiesAsync(string ownerId);

       public Task<Property> GetPropertyDetailsAsync(int id);

       public Task AddPropertyMediaAsync(PropertyMedia media);

       public Task RemovePropertyMediaAsync(int mediaId);

        // Update property status (Active / Inactive / Pending)
       public Task UpdatePropertyStatusAsync(int id, PropEnum status);
       public Task SendMessageAsync(Chat chat);
        public Task<IEnumerable<Chat>> GetChatHistoryAsync(string senderId, string receiverId, int propertyId);

        Task<IEnumerable<Property>> GetFilteredPropertiesAsync(PropertyFilterDTO filter);

        Task<IEnumerable<PropertyComparisonDTO>> GetPropertiesForComparisonAsync(List<int> propertyIds);
    }
}
