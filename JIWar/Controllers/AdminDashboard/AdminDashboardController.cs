using Jiwar.DTOs.AccountDTOs.EditProfileDtos;
using Jiwar.Enum;
using Jiwar.Repositories;
using Jiwar.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Jiwar.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/dashboard")]
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IPropertyRepository _propertyRepo;
        private readonly IWishlistRepository _wishlistRepo;
        private readonly IAdminAnalyticsService _analyticsService;
      

        public AdminDashboardController(
            IUserRepository userRepo,
            IPropertyRepository propertyRepo,
            IWishlistRepository wishlistRepo,
            IAdminAnalyticsService adminService)
        {
            _userRepo = userRepo;
            _propertyRepo = propertyRepo;
            _wishlistRepo = wishlistRepo;
            _analyticsService = adminService;
        }

        //  USERS
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepo.GetAllUsersForAdminAsync();
            return Ok(users);
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userRepo.DeleteUserAsync(id);
            return Ok("User deleted successfully");
        }

        // PROPERTIES
        [HttpGet("properties")]
        public async Task<IActionResult> GetAllProperties()
        {
            var properties = await _propertyRepo.GetAllPropertiesForAdminAsync();
            return Ok(properties);
        }

        [HttpPut("properties/{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePropertyStatus(
       int id,
       PropEnum statusEnum)
        {
            await _propertyRepo.UpdatePropertyStatusAsync(id, statusEnum);
           

            return Ok("Property status updated successfully");
        }


        // WISHLIST
        [HttpGet("wishlists")]
        public async Task<IActionResult> GetAllWishlists()
        {
            var list = await _wishlistRepo.GetAllWishlistsForAdminAsync();
            return Ok(list);
        }

     
     
    }
}
