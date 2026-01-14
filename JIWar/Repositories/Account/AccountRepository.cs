using GEWAR;
using GEWAR.Models;
using Jiwar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace Jiwar.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly GiwarContext _context;

        public AccountRepository(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> _roleManager, GiwarContext _context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this._roleManager = _roleManager;
            this._context = _context;
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

        public Task<bool> RoleExistsAsync(string roleName)
        {
            return _roleManager.RoleExistsAsync(roleName); 
        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            await _userManager.AddToRoleAsync(user, roleName); 
        }

        public async Task AddPropertyOwnerAsync(PropertyOwner owner)
        {
            await _context.PropertyOwners.AddAsync(owner);
            await _context.SaveChangesAsync();
        }

        public async Task AddInteriorDesignerAsync(InteriorDesigner designer)
        {
            await _context.InteriorDesigners.AddAsync(designer);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> PropertyOwnerExistsAsync(string userId)
        {
            return await _context.PropertyOwners.AnyAsync(po =>
       po.UserID == userId 
       );
        }

        public Task<bool> InteriorDesignerExistsAsync(string userId)
        {
            return _context.InteriorDesigners.AnyAsync(id => id.InteriorDesignerID == userId);
        }
        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _context.Users
                .Include(u => u.propertyOwner)
                    .ThenInclude(po => po.Properties)
                .Include(u => u.InteriorDesigner)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task<PropertyOwner?> GetPropertyOwnerByUserIdAsync(string userId)
        {
            return await _context.PropertyOwners
                .FirstOrDefaultAsync(po => po.UserID == userId);
        }

        public async Task UpdatePropertyOwnerAsync(PropertyOwner owner)
        {
            _context.PropertyOwners.Update(owner);
            await _context.SaveChangesAsync();
        }
        public async Task<PropertyOwner?> GetPropertyOwnerPublicAsync(string userId)
        {
            return await _context.PropertyOwners
        .Include(po => po.Owneruser)
        .AsNoTracking()
        .FirstOrDefaultAsync(po => po.UserID == userId);
        }
        public async Task<InteriorDesigner?> GetInteriorDesignerByUserIdAsync(string userId)
        {
            return await _context.InteriorDesigners
                .Include(id => id.User)
                .FirstOrDefaultAsync(id => id.InteriorDesignerID == userId);
        }

        public async Task UpdateInteriorDesignerAsync(InteriorDesigner designer)
        {
            _context.InteriorDesigners.Update(designer);
            await _context.SaveChangesAsync();
        }


    }



}
