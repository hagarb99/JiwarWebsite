using GEWAR.Models;
using Jiwar.Account.DTOs;
using Jiwar.Account.Services;
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

        public AccountController(IAccountService accountService, IConfiguration config,UserManager<User> userManager, IPropertyService _propertyService)
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
                return Ok(userResponse); // JSON response
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var userResponse = await accountService.LoginAsync(dto);
                if (!userResponse.Success)
                    return Unauthorized(userResponse);
                return Ok(new
                {
                    user = userResponse.Data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("forget-password")]
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


        //[Authorize]
        //[HttpPost("change-password")]
        //public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    if (string.IsNullOrEmpty(userId))
        //        return Unauthorized(new { message = "User ID not found in token." });

        //    var user = await userManager.FindByIdAsync(userId);

        //    if (user == null)
        //        return Unauthorized(new { message = "User not found." });

        //    var result = await accountService.ChangePasswordAsync(user, dto);

        //    if (!result.Success)
        //        return BadRequest(new { message = result.Message });

        //    return Ok(new { message = result.Message });
        //}

        //[Authorize]
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
        public async Task<IActionResult> EditProfile([FromBody] EditProfileDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = User;
            try
            {
                var updatedUser = await accountService.EditProfileAsync(dto);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{propertyId}/chat/send")]
        public async Task<IActionResult> SendMessage(int propertyId, ChatMessageDTO dto)
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


    }
}
