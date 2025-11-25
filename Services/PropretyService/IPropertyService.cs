using Jiwar.Models;
using Jiwar.Enum;

namespace Jiwar.Service
{
    public interface IPropertyService
    {
        // Add a new property
        Task<Property> AddPropertyAsync(Property property);

        // Update an existing property
        Task<bool> UpdatePropertyAsync(Property property);

        // Soft delete property
        Task<bool> DeletePropertyAsync(int id);

        // Get all properties of an owner
        Task<IEnumerable<Property>> GetMyPropertiesAsync(string ownerId);

        // Get details of a specific property
        Task<Property> GetPropertyDetailsAsync(int id);

        // Add media to a property
        Task AddPropertyMediaAsync(PropertyMedia media);

        // Remove media from a property
        Task RemovePropertyMediaAsync(int mediaId);

        // Update property status (Active / Inactive / Pending)
        Task UpdatePropertyStatusAsync(int id, PropEnum status);

    }
}
