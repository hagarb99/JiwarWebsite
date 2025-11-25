using Jiwar.DTOs.WishlistDTOs;
using Jiwar.Repositories;

namespace Jiwar.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _repo;

        public WishlistService(IWishlistRepository repo)
        {
            _repo = repo;
        }

        public async Task AddToWishlist(string userId, int propertyId, string? notes = null)
        {
            try
            {
                await _repo.AddAsync(userId, propertyId, notes);
            }
            catch (Exception ex)
            {
                // Re-throw exception so the controller can return proper HTTP response
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<WishlistDto>> GetWishlist(string userId)
        {
            return await _repo.GetUserWishlist(userId);
        }

        public async Task<bool> RemoveFromWishlist(string userId, int propertyId)
        {
            return await _repo.RemoveAsync(userId, propertyId);
        }
    }
}

