using GEWAR.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Jiwar.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public Task<IdentityResult> CreateUserAsync(User user, string password)
       => _userManager.CreateAsync(user, password);

        public Task<User?> FindByEmailAsync(string email)
            => _userManager.FindByEmailAsync(email);

        public Task<User?> FindByIdAsync(string id)
            => _userManager.FindByIdAsync(id);

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result.Succeeded;
        }

        public Task<string> GenerateResetTokenAsync(User user)
            => _userManager.GeneratePasswordResetTokenAsync(user);

        public Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
            => _userManager.ResetPasswordAsync(user, token, newPassword);

        public Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
            => _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        public Task<IdentityResult> UpdateUserAsync(User user)
            => _userManager.UpdateAsync(user);

        public Task<User?> GetUserFromClaimsAsync(ClaimsPrincipal userClaims)
        {
            return _userManager.GetUserAsync(userClaims);
        }
    }



}
