using Jiwar.Models;
using Jiwar.Enum;
using GEWAR.Models;

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
       public Task<IEnumerable<Property>> GetMyPropertiesAsync(string ownerId);

        // Get details of a specific property
       public Task<Property> GetPropertyDetailsAsync(int id);

        // Add media to a property
       public Task AddPropertyMediaAsync(PropertyMedia media);

        // Remove media from a property
       public Task RemovePropertyMediaAsync(int mediaId);

        // Update property status (Active / Inactive / Pending)
       public Task UpdatePropertyStatusAsync(int id, PropEnum status);

    }
}
