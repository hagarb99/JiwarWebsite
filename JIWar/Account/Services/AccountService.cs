
using GEWAR.Models;
using Jiwar.Account.DTOs;
using Jiwar.Helpers;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Jiwar.Account.Services
{
    public class AccountService 
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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
            var result = await userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                //return ResultViewModel<string>
                //      .Fail(string.Join("; ", result.Errors.Select(e => e.Description)));

          return  ResultViewModel<UserResponseDTO>.Fail(
        string.Join("; ", result.Errors.Select(e => e.Description))
    );

            // Assign role to the user
            //await userManager.AddToRoleAsync(user, dto.Role);
            // Get the role from Identity
            //var roles = await userManager.GetRolesAsync(user);
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
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return ResultViewModel<UserResponseDTO>.Fail("Invalid email or password.");

            var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
              return  ResultViewModel<UserResponseDTO>.Fail("Your Account under reviewing");

            ////////////////////////////////////////////////////////////
            //roles
            //var roles = await userManager.GetRolesAsync(user);
            return ResultViewModel<UserResponseDTO>.Ok("Login successful.",
                new UserResponseDTO
                {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ProfilePicURL = user.ProfilePicURL,
                Role = user.Role

            });
        }

        public async Task<ResultViewModel<string>> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var user = await userManager.FindByIdAsync(dto.UserId.ToString());
            if (user == null)
             return  ResultViewModel<string>.Fail("user not found");

            var result = await userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!result.Succeeded)
               return ResultViewModel<string>.Fail(string.Join("; ", result.Errors.Select(e => e.Description)));

            return ResultViewModel<string>.Ok("Password changed successfully.", "");
        }
        public async Task<ResultViewModel<string>> ForgetPasswordAsync(ForgetPasswordDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return ResultViewModel<string>.Fail("Email not found.");

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            return ResultViewModel<string>.Ok("Reset token generated.", token);
        }
        public async Task<ResultViewModel<string>> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return ResultViewModel<string>.Fail("Invalid email.");

            var result = await userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);

            if (!result.Succeeded)
                return ResultViewModel<string>.Fail("Failed to reset password.");

            return ResultViewModel<string>.Ok("Password reset successfully.", "");
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
                ResultViewModel<string>.Fail(string.Join("; ", result.Errors.Select(e => e.Description)));


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
