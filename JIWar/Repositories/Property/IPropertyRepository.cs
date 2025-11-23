using GEWAR.Models;
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
}