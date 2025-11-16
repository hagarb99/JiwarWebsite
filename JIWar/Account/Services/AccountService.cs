
using GEWAR.Models;
using Jiwar.Account.DTOs;
using Jiwar.Helpers;
using Microsoft.AspNetCore.Identity;

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

            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                //UserType = user.UserTypeEnum
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

            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ProfilePicURL = user.ProfilePicURL
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



    }
}
