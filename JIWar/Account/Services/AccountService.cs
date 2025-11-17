
using GEWAR.Models;
using Jiwar.Account.DTOs;
using Jiwar.Helpers;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Jiwar.Account.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<UserResponseDTO> RegisterAsync(RegisterDto dto)
        {
            var user = new User
            {
                UserName = dto.Username,
                Name = dto.Name,
                Email = dto.Email,
                //UserTypeEnum = dto.UserType,
                PhoneNumber = dto.PhoneNumber,
                RegistrationDate = DateTime.UtcNow
            };
            var result = await userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));

            // Assign role to the user
            await userManager.AddToRoleAsync(user, dto.Role.ToString());
            // Get the role from Identity
            var roles = await userManager.GetRolesAsync(user);
            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = roles.FirstOrDefault()
            };

        }

        public async Task<UserResponseDTO> LoginAsync(LoginDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                throw new Exception("Invalid email or password.");

            var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                throw new Exception("Invalid email or password.");
            //roles
            var roles = await userManager.GetRolesAsync(user);
            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ProfilePicURL = user.ProfilePicURL,
                Role = roles.FirstOrDefault()

            };
        }

        public async Task ChangePasswordAsync(ChangePasswordDto dto)
        {
            var user = await userManager.FindByIdAsync(dto.UserId.ToString());
            if (user == null)
                throw new Exception("User not found");

            var result = await userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!result.Succeeded)
                throw new Exception("Password change failed");
        }
        public async Task<ServiceResult> ForgetPasswordAsync(ForgetPasswordDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return ServiceResult.Failed("Email not found.");

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            return ServiceResult.Succeeded("Reset token generated.", token);
        }
        public async Task<ServiceResult> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return ServiceResult.Failed("Invalid email.");

            var result = await userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);

            if (!result.Succeeded)
                return ServiceResult.Failed("Failed to reset password.", result.Errors);

            return ServiceResult.Succeeded("Password reset successfully.");
        }

        public async Task<UserResponseDTO> EditProfileAsync(EditProfileDto dto)
        {
            var user = await userManager.FindByIdAsync(dto.UserId);
            if (user == null)
                throw new Exception("User not found");

            if (!string.IsNullOrEmpty(dto.Name))
                user.Name = dto.Name;
            if (!string.IsNullOrEmpty(dto.Email))
                user.Email = dto.Email;
            if (!string.IsNullOrEmpty(dto.PhoneNumber))
                user.PhoneNumber = dto.PhoneNumber;
            if (!string.IsNullOrEmpty(dto.ProfilePicURL))
                user.ProfilePicURL = dto.ProfilePicURL;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));

            var roles = await userManager.GetRolesAsync(user);

            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ProfilePicURL = user.ProfilePicURL,
                Role = roles.FirstOrDefault()
            };
        }

    }
}
