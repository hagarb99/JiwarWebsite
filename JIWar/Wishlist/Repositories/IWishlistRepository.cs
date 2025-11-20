using Jiwar.DTOs.WishlistDTOs;

namespace Jiwar.Repositories
{
    public interface IWishlistRepository
    {
        Task AddAsync(AddWishlistDto dto);
        Task<List<WishlistDto>> GetUserWishlist(string userId);
        Task<bool> RemoveAsync(int id);
    }

}
