using GEWAR.Models;
using Jiwar.DTOs.PropertyDTOs;
using Jiwar.Enum;
using Jiwar.Models;
using Jiwar.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPropertyRepository : IGenericRepository<Property>
{
    Task<IEnumerable<Property>> GetMyPropertiesAsync(string ownerId);
    Task<Property> GetPropertyDetailsAsync(int id);
    Task AddPropertyMediaAsync(PropertyMedia media);
    Task RemovePropertyMediaAsync(int mediaId);
    Task UpdatePropertyStatusAsync(int id,  PropEnum statusEnum);
    Task SendMessageAsync(Chat chat);
    Task<IEnumerable<Chat>> GetChatHistoryAsync(string senderId, string receiverId, int propertyId);

    Task<IEnumerable<Property>> GetFilteredPropertiesAsync(PropertyFilterDTO filter);
    Task<IEnumerable<Property>> GetPropertiesByIdsAsync(List<int> ids);
    Task<decimal> GetCityAveragePricePerSqmAsync(string city);
    Task<List<Property>> GetComparablePropertiesAsync(string city, decimal v1, string v2, int areaTolerancePercentage, int ageToleranceYears, int minComps);
    Task UpdateAsync(Property property);
    Task<PropertyOwner> GetOwnerByIdAsync(string ownerId);
    Task<PropertyOwner> CreateOwnerAsync(string ownerId);
}