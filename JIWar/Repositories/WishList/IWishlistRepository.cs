using Jiwar.DTOs.WishlistDTOs;

namespace Jiwar.Repositories
{
    public interface IWishlistRepository
    {
        Task AddAsync(string userId, int propertyId, string? notes = null);
        Task<List<WishlistDto>> GetUserWishlist(string userId);
        Task<bool> RemoveAsync(string userId, int propertyId);
    }
}

