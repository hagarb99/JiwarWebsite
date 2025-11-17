using Jiwar.DTOs;
using Jiwar.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jiwar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistRepository _repo;

        public WishlistController(IWishlistRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist([FromBody] AddWishlistDto dto)
        {
            await _repo.AddAsync(dto);
            return Ok("Added to wishlist");
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserWishlist(string userId)
        {
            var items = await _repo.GetUserWishlist(userId);
            return Ok(items);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveWishlistItem(int id)
        {
            var removed = await _repo.RemoveAsync(id);
            if (!removed)
                return NotFound("Wishlist item not found");

            return Ok("Item removed from wishlist");
        }
    }

}
