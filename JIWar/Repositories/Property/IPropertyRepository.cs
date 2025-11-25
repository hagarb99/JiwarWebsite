using GEWAR.Models;
using Jiwar.Enum;
using Jiwar.Models;
using Jiwar.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPropertyRepository : IGenericRepository<Property>
{
    Task<IEnumerable<Property>> GetMyPropertiesAsync(int ownerId);
    Task<Property> GetPropertyDetailsAsync(int id);
    Task AddPropertyMediaAsync(PropertyMedia media);
    Task RemovePropertyMediaAsync(int mediaId);
    Task UpdatePropertyStatusAsync(int id,  PropEnum statusEnum);
    Task SendMessageAsync(Chat chat);
    Task<IEnumerable<Chat>> GetChatHistoryAsync(string senderId, string receiverId, int propertyId);
}