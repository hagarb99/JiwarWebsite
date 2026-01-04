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

       public Task<bool> UpdatePropertyAsync(PropertyUpdateDTO dto);

       public Task<bool> DeletePropertyAsync(int id);

        // Get all properties of an owner
       public Task<IEnumerable<PropertyListBDTO>> GetMyPropertiesAsync(string ownerId);

       public Task AddPropertyMediaAsync(PropertyMedia media);

       public Task RemovePropertyMediaAsync(int mediaId);

        // Update property status (Active / Inactive / Pending)
       public Task UpdatePropertyStatusAsync(int id, PropEnum status);
       public Task SendMessageAsync(Chat chat);
        public Task<IEnumerable<Chat>> GetChatHistoryAsync(string senderId, string receiverId, int propertyId);

       public Task<IEnumerable<PropertyListBDTO>> GetFilteredPropertiesAsync(PropertyFilterDTO filter);

        public Task<IEnumerable<PropertyComparisonDTO>> GetPropertiesForComparisonAsync(List<int> propertyIds);
       public Task<PagedResult<PropertyListBDTO>> GetAllPropertiesAsync(int page, int pageSize);
       public Task<PropertyDetailsDTO> GetPropertyDetailsDTOAsync(int id);
    }
}
