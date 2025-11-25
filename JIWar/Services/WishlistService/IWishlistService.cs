using Jiwar.DTOs.WishlistDTOs;

namespace Jiwar.Services
{
    public interface IWishlistService
    {
        Task AddToWishlist(string userId, int propertyId, string? notes = null);
        Task<List<WishlistDto>> GetWishlist(string userId);
        Task<bool> RemoveFromWishlist(string userId, int propertyId);
    }
}
