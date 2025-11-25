using Jiwar.DTOs.WishlistDTOs;
using GEWAR.Models;
using Microsoft.EntityFrameworkCore;
using GEWAR;

namespace Jiwar.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly GiwarContext _context;

        public WishlistRepository(GiwarContext context)
        {
            _context = context;
        }

        public async Task AddAsync(string userId, int propertyId, string? notes = null)
        {
            // Check if property exists in Properties table
            var propertyExists = await _context.Properties.AnyAsync(p => p.Id == propertyId);
            if (!propertyExists)
                throw new Exception("Property does not exist.");

            // Check if already in wishlist
            var alreadyAdded = await _context.WishLists
                .AnyAsync(w => w.UserID == userId && w.PropertyID == propertyId);
            if (alreadyAdded)
                throw new Exception("Property is already in wishlist.");

            var item = new WishList
            {
                UserID = userId,
                PropertyID = propertyId,
                Notes = notes,
                AddedDate = DateTime.UtcNow
            };

            _context.WishLists.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<WishlistDto>> GetUserWishlist(string userId)
        {
            return await _context.WishLists
                .Where(w => w.UserID == userId)
                .Select(w => new WishlistDto
                {
                    Id = w.Id,
                    UserID = w.UserID,
                    PropertyID = w.PropertyID,
                    Notes = w.Notes,
                    AddedDate = w.AddedDate
                })
                .ToListAsync();
        }

        public async Task<bool> RemoveAsync(string userId, int propertyId)
        {
            var item = await _context.WishLists
                .FirstOrDefaultAsync(w => w.UserID == userId && w.PropertyID == propertyId);

            if (item == null) return false;

            _context.WishLists.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
