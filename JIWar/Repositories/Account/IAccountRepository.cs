using GEWAR.Models;
using Jiwar.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Jiwar.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(User user, string password);
        Task<User?> FindByEmailAsync(string email);
        Task<User?> FindByIdAsync(string id);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<string> GenerateResetTokenAsync(User user);
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
        Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);
        Task<IdentityResult> UpdateUserAsync(User user);
        Task<User?> GetUserFromClaimsAsync(ClaimsPrincipal userClaims);
        Task<bool> RoleExistsAsync(string roleName);
        Task AddUserToRoleAsync(User user, string roleName);
        Task AddPropertyOwnerAsync(PropertyOwner owner);
        Task AddInteriorDesignerAsync(InteriorDesigner designer);
        Task<bool> PropertyOwnerExistsAsync(string userId);
        Task<bool> InteriorDesignerExistsAsync(string userId);
        Task<User> GetUserByIdAsync(string userId);
        Task<PropertyOwner?> GetPropertyOwnerByUserIdAsync(string userId);
        Task UpdatePropertyOwnerAsync(PropertyOwner owner);
        Task<PropertyOwner?> GetPropertyOwnerPublicAsync(string userId);
        Task<InteriorDesigner?> GetInteriorDesignerByUserIdAsync(string userId);
        Task UpdateInteriorDesignerAsync(InteriorDesigner designer);

    }
}
