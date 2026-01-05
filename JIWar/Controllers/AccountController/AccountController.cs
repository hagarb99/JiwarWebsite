using AutoMapper;
using GEWAR.Models;
using Jiwar.Account.DTOs;
using Jiwar.Account.Services;
using Jiwar.DTOs;
using Jiwar.DTOs.AccountDTOs.EditProfileDtos;
using Jiwar.DTOs.AccountDTOs.EditProfileDtos;
using Jiwar.DTOs.ChatDTOs;
using Jiwar.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Jiwar.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IConfiguration _config;
        private readonly UserManager<User> userManager;
        private readonly IPropertyService _propertyService;
        public AccountController(
            IAccountService accountService,
            IConfiguration config,
            UserManager<User> userManager,
            IPropertyService _propertyService)
        {
            this.accountService = accountService;
            this._config = config;
            this.userManager = userManager;
            this._propertyService = _propertyService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (dto == null)
                    return BadRequest("Request body is empty");

                var userResponse = await accountService.RegisterAsync(dto);

                if (!userResponse.Success)
                    return BadRequest(userResponse);

                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            //if (!ModelState.IsValid) return BadRequest(ModelState);
            //var userResponse = await accountService.RegisterAsync(dto);
            //return userResponse.Success ? Ok(userResponse) : BadRequest(userResponse);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userResponse = await accountService.LoginAsync(dto);

            if (!userResponse.Success)
            {
                // رجع 401 مع رسالة واضحة فقط (مش الكائن كله)
                return Unauthorized(new { message = userResponse.Message ?? "Invalid email or password" });
            }

            // رجع البيانات مع التوكن بشكل نظيف
            return Ok(new
            {
                token = userResponse.Data.Token,
                id = userResponse.Data.Id,
                name = userResponse.Data.Name,
                email = userResponse.Data.Email,
                profilePicURL = userResponse.Data.ProfilePicURL,
                role = userResponse.Data.Role,
                isProfileCompleted = userResponse.Data.IsProfileCompleted
            });
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);
            //    var userResponse = await accountService.LoginAsync(dto);

            //    if (!userResponse.Success)
            //{
            //    return Unauthorized(userResponse);
            //}

            //return Ok(userResponse.Data);
        }


        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordDto dto)
        {
            var result = await accountService.ForgetPasswordAsync(dto);
            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            dto.Token = dto.Token.Replace(" ", "+");
            var result = await accountService.ResetPasswordAsync(dto);
            return Ok(result);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await accountService.ChangePasswordAsync(User, dto);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }


        [Authorize]
        [HttpPut("edit-profile")]
        public async Task<IActionResult> EditProfile([FromBody] EditProfileBaseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await accountService.EditProfileAsync(User, dto);

            return Ok(result);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("profile/customer")]
        public async Task<IActionResult> UpdateCustomerProfile([FromBody] CustomerEditProfileDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User ID not found in token." });

            await accountService.UpdateCustomerProfileAsync(userId, dto);
            return NoContent();
        }

        [HttpPost("{propertyId}/chat/send")]
        public async Task<IActionResult> SendMessage(int propertyId, [FromBody] ChatMessageDTO dto)
        {
            var chat = new Chat
            {
                PropertyID = propertyId,
                SenderID = dto.SenderID,
                ReceiverID = dto.ReceiverID,
                MessageText = dto.MessageText,
                MessageType = dto.MessageType,
                SentDate = DateTime.UtcNow
            };

            await _propertyService.SendMessageAsync(chat);
            return Ok("Message sent successfully");
        }

        [HttpGet("{propertyId}/chat/{senderId}/{receiverId}")]
        public async Task<IActionResult> GetChatHistory(int propertyId, string senderId, string receiverId)
        {
            var history = await _propertyService.GetChatHistoryAsync(senderId, receiverId, propertyId);
            return Ok(history);
        }


        [Authorize(Roles = "PropertyOwner")]
        [HttpPut("complete-profile/property-owner")]
        public async Task<IActionResult> CompletePropertyOwnerProfile([FromBody] PropertyOwnerEditProfileDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token.");

            await accountService.UpdatePropertyOwnerProfileAsync(userId, dto);

            return Ok("PropertyOwner profile completed successfully.");
        }

        [Authorize(Roles = "InteriorDesigner")]
        [HttpPost("complete-profile/interior-designer")]
        public async Task<IActionResult> CompleteInteriorDesignerProfile([FromBody] InteriorDesignerEditProfileDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized("User ID not found in token.");

            await accountService.UpdateInteriorDesignerProfileAsync(userId, dto);
            return Ok("InteriorDesigner profile completed successfully.");
        }

        [HttpPost("google-signin")]
        public async Task<IActionResult> GoogleSignIn([FromBody] GoogleSignInRequest req)
        {
            if (string.IsNullOrEmpty(req?.IdToken))
                return BadRequest("IdToken is required");

            var result = await accountService.GoogleSignInAsync(req.IdToken);

            return result.Success
                ? Ok(result)
                : BadRequest(result);
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            // جلب الـ UserId من التوكن
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User ID not found in token." });

            // استدعاء الـ Service لجلب بيانات البروفايل
            var profile = await accountService.GetUserProfileAsync(userId);

            if (profile == null)
                return NotFound(new { message = "Profile not found." });

            return Ok(profile);
        }

        [HttpGet("property-owner/{userId}/public-profile")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyOwnerPublicProfile(string userId)
        {
            var profile = await accountService.GetPropertyOwnerPublicProfileAsync(userId);

            if (profile == null)
                return NotFound(new { message = "Property owner not found" });

            return Ok(profile);
        }



    }
}
