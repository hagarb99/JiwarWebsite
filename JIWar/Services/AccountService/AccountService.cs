
using GEWAR.Models;
using Jiwar.Account.DTOs;
using Jiwar.Controllers;
using Jiwar.Helpers;
using Jiwar.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Security.Claims;

namespace Jiwar.Account.Services
{
    public class AccountService : IAccountService
    {
        //private readonly UserManager<User> userManager;
        //private readonly SignInManager<User> signInManager;
        private readonly IAccountRepository repo;
        private readonly TokenService _tokenService;
        public AccountService(IAccountRepository repo, TokenService tokenService)
        {
            this.repo = repo;
            _tokenService = tokenService;
        }
        public async Task<ResultViewModel<UserResponseDTO>> RegisterAsync(RegisterDto dto)
        {
            var user = new User
            {
                UserName = dto.Username,
                Name = dto.Name,
                Email = dto.Email,
                Role = dto.Role,
                PhoneNumber = dto.PhoneNumber,
                RegistrationDate = DateTime.UtcNow
            };
            var result = await repo.CreateUserAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                return ResultViewModel<UserResponseDTO>.Fail(
         string.Join("; ", result.Errors.Select(e => e.Description))
     );

            }
       
            return ResultViewModel<UserResponseDTO>.Ok("User registered successfully.", 
                new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
            });

        }

        public async Task<ResultViewModel<UserResponseDTO>> LoginAsync(LoginDto dto)
        {
            var user = await repo.FindByEmailAsync(dto.Email);
            if (user == null)
                return ResultViewModel<UserResponseDTO>.Fail("Invalid email or password.");

            //var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            //if (!result.Succeeded)
            //  return  ResultViewModel<UserResponseDTO>.Fail("Your Account under reviewing");

            ////////////////////////////////////////////////////////////
            //roles
            //var roles = await userManager.GetRolesAsync(user);
            var token = _tokenService.CreateToken(user);

            return ResultViewModel<UserResponseDTO>.Ok("Login successful.",
                new UserResponseDTO
                {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ProfilePicURL = user.ProfilePicURL,
                Role = user.Role,
                Token = token
                });
        }

        //      public async Task<ResultViewModel<string>> ChangePasswordAsync(User user,ChangePasswordDto dto)
        //{
        //        //    var user = await userManager.FindByIdAsync(dto.UserId.ToString());

        //        //    if (user == null)
        //        //return ResultViewModel<string>.Fail("User not found.");

        //    var result = await repo.ChangePasswordAsync(
        //        user,
        //        dto.CurrentPassword,
        //        dto.NewPassword
        //    );

        //    if (!result.Succeeded)
        //        return ResultViewModel<string>.Fail(
        //            string.Join("; ", result.Errors.Select(e => e.Description)));

        //    return ResultViewModel<string>.Ok("Password changed successfully.", "");
        //}
        public async Task<ResultViewModel<string>> ChangePasswordAsync(ClaimsPrincipal userClaims, ChangePasswordDto dto)
        {
            var user = await repo.GetUserFromClaimsAsync(userClaims);
            if (user == null)
                return ResultViewModel<string>.Fail("User not found.");

            var result = await repo.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);

            if (!result.Succeeded)
                return ResultViewModel<string>.Fail(string.Join("; ", result.Errors.Select(e => e.Description)));

            return ResultViewModel<string>.Ok("Password changed successfully.", "");
        }


        public async Task<ResultViewModel<string>> ForgetPasswordAsync(ForgetPasswordDto dto)
        {
            var user = await repo.FindByEmailAsync(dto.Email);

            if (user == null)
                return ResultViewModel<string>.Fail("Email not found.");

            var token = await repo.GenerateResetTokenAsync(user);

            return ResultViewModel<string>.Ok("Reset token generated.", token);
        }
        public async Task<ResultViewModel<string>> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await repo.FindByEmailAsync(dto.Email);

            if (user == null)
                return ResultViewModel<string>.Fail("Invalid email.");

            var result = await repo.ResetPasswordAsync(user, dto.Token, dto.NewPassword);

            if (!result.Succeeded)
                return ResultViewModel<string>.Fail("Failed to reset password.");

            return ResultViewModel<string>.Ok("Password reset successfully.", "");
        }

        public async Task<ResultViewModel<UserResponseDTO>> EditProfileAsync(EditProfileDto dto)
        {
            var user = await repo.FindByIdAsync(dto.UserId);
            if (user == null)
                return ResultViewModel<UserResponseDTO>.Fail("User not found.");

            if (!string.IsNullOrEmpty(dto.Name))
                user.Name = dto.Name;
            if (!string.IsNullOrEmpty(dto.Email))
                user.Email = dto.Email;
            if (!string.IsNullOrEmpty(dto.PhoneNumber))
                user.PhoneNumber = dto.PhoneNumber;
            if (!string.IsNullOrEmpty(dto.ProfilePicURL))
                user.ProfilePicURL = dto.ProfilePicURL;

            var result = await repo.UpdateUserAsync(user);
            if (!result.Succeeded)
                return ResultViewModel<UserResponseDTO>.Fail("Failed to update profile.");
            return ResultViewModel<UserResponseDTO>.Ok("Profile updated successfully.", new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                ProfilePicURL = user.ProfilePicURL
            });

        }

    }
}
