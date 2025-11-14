
using GEWAR.Models;
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
    }
}
