using System;
using GEWAR;
using GEWAR.Models;
using Jiwar.DTOs.WishlistDTOs;
using Microsoft.EntityFrameworkCore;

namespace Jiwar.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly GiwarContext _context;

        public WishlistRepository(GiwarContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AddWishlistDto dto)
        {
            var item = new WishList
            {
                UserID = dto.UserID,
                PropertyID = dto.PropertyID,
                Notes = dto.Notes,
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
                    AddedDate = w.AddedDate,
                    Notes = w.Notes
                })
                .ToListAsync();
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var item = await _context.WishLists.FindAsync(id);

            if (item == null)
                return false;

            _context.WishLists.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
