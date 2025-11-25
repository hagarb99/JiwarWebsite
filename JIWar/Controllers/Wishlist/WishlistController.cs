using System.Security.Claims;
using Jiwar.DTOs.WishlistDTOs;
using Jiwar.Repositories;
using Jiwar.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jiwar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _service;

public WishlistController(IWishlistService service)
{
    _service = service;
}

[HttpPost]
public async Task<IActionResult> AddToWishlist([FromBody] AddWishlistDto dto)
{
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (userId == null) return Unauthorized("User not logged in");

    try
    {
        await _service.AddToWishlist(userId, dto.PropertyID, dto.Notes);
        return Ok("Added to wishlist");
    }
    catch (InvalidOperationException ex)
    {
        return BadRequest(ex.Message);
    }
}


[HttpGet]
public async Task<IActionResult> GetUserWishlist()
{
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (userId == null) return Unauthorized("User not logged in");

    var items = await _service.GetWishlist(userId);
    return Ok(items);
}

[HttpDelete("{propertyId}")]
public async Task<IActionResult> RemoveWishlistItem(int propertyId)
{
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (userId == null) return Unauthorized("User not logged in");

    var removed = await _service.RemoveFromWishlist(userId, propertyId);
    if (!removed) return NotFound("Item not found or not for this user");

    return Ok("Item removed from wishlist");
}

    }

}
