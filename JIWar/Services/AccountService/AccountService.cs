
using GEWAR.Models;
using Jiwar.Account.DTOs;
using Jiwar.Controllers;
using Jiwar.DTOs.AccountDTOs.EditProfileDtos;
using Jiwar.Helpers;
using Jiwar.Models;
using Jiwar.Repositories;
using Jiwar.Services.GoogleService;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Security.Claims;

namespace Jiwar.Account.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository repo;
        private readonly TokenService _tokenService;
        private readonly GoogleAuthService _googleAuthService;
        private readonly UserManager<User> _userManager;
        public AccountService(IAccountRepository repo, TokenService tokenService , GoogleAuthService _googleAuthService,
            UserManager<User> _userManager
            )
        {
            this.repo = repo;
            _tokenService = tokenService;
            this._googleAuthService = _googleAuthService;
            this._userManager = _userManager;
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

            await repo.AddUserToRoleAsync(user, dto.Role);

            //if (dto.Role == "PropertyOwner")
            //{
            //    var owner = new PropertyOwner { UserID = user.Id };
            //    await repo.AddPropertyOwnerAsync(owner);
            //}
            //else if (dto.Role == "InteriorDesigner")
            //{
            //    var designer = new InteriorDesigner { InteriorDesignerID = user.Id };
            //    await repo.AddInteriorDesignerAsync(designer);
            //}
            await repo.AddUserToRoleAsync(user, dto.Role);

            return ResultViewModel<UserResponseDTO>.Ok(
                "User registered successfully.",
                new UserResponseDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role
                }
            );


        }

        public async Task<ResultViewModel<UserResponseDTO>> LoginAsync(LoginDto dto)
        {
            var user = await repo.FindByEmailAsync(dto.Email);
            if (user == null)
                return ResultViewModel<UserResponseDTO>.Fail("Invalid email or password.");

            var isPasswordValid = await repo.CheckPasswordAsync(user, dto.Password);
            if (!isPasswordValid)
                return ResultViewModel<UserResponseDTO>.Fail("Invalid email or password.");

            var token = await _tokenService.CreateTokenAsync(user);

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            bool isProfileCompleted = true;
            if (role == "PropertyOwner")
                isProfileCompleted = await repo.PropertyOwnerExistsAsync(user.Id);

            else if (role == "InteriorDesigner")
                isProfileCompleted = await repo.InteriorDesignerExistsAsync(user.Id);

            return ResultViewModel<UserResponseDTO>.Ok("Login successful.",
                new UserResponseDTO
                {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ProfilePicURL = user.ProfilePicURL,
                Role = user.Role,
                Token = token,
                IsProfileCompleted = isProfileCompleted
                });
        }

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

        public async Task<ResultViewModel<UserResponseDTO>> EditProfileAsync(ClaimsPrincipal userClaims, EditProfileBaseDto dto)
        {
            
            var user = await repo.GetUserFromClaimsAsync(userClaims);

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

            return ResultViewModel<UserResponseDTO>.Ok(
                "Profile updated successfully.",
                new UserResponseDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role,
                    ProfilePicURL = user.ProfilePicURL
                }
            );
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await repo.RoleExistsAsync(roleName);
        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await repo.AddUserToRoleAsync(user, roleName);
        }

        public async Task AddPropertyOwnerAsync(PropertyOwner owner)
        {
            await repo.AddPropertyOwnerAsync(owner);
        }

        public async Task AddInteriorDesignerAsync(InteriorDesigner designer)
        {
            await repo.AddInteriorDesignerAsync(designer);
        }

        public async Task<ResultViewModel<UserResponseDTO>> GoogleSignInAsync(string idToken)
        {
            var payload = await _googleAuthService.VerifyGoogleTokenAsync(idToken);
            if (payload == null)
                return ResultViewModel<UserResponseDTO>.Fail("Invalid Google token");

            var user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                user = new User
                {
                    UserName = payload.Email,
                    Email = payload.Email,
                    GoogleId = payload.Subject,
                    Name = payload.Name,
                    ProfilePicURL = payload.Picture,
                    RegistrationDate = DateTime.UtcNow,
                    Role = "Customer"
                };
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                    return ResultViewModel<UserResponseDTO>.Fail(string.Join("; ", result.Errors.Select(e => e.Description)));
            }

            var token = await _tokenService.CreateTokenAsync(user);

            return ResultViewModel<UserResponseDTO>.Ok("Login successful", new UserResponseDTO
            {
                Email = user.Email,
                Name = user.Name,
                GoogleId = user.GoogleId,
                Role = user.Role,
                Token = token
            });
        }

        public Task UpdateCustomerProfileAsync(string userId, CustomerEditProfileDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task UpdatePropertyOwnerProfileAsync(string userId, PropertyOwnerEditProfileDto dto)
        {
            //var owner  = new PropertyOwner
            //{

            //};
            //await repo.AddPropertyOwnerAsync(owner);
            throw new NotImplementedException();
        }

        public async Task UpdateInteriorDesignerProfileAsync(string userId, InteriorDesignerEditProfileDto dto)
        {
            //var designer = new InteriorDesigner
            //{

            //};
            //await repo.AddInteriorDesignerAsync(designer);
            throw new NotImplementedException();
        }

        public Task UpdateAdminProfileAsync(string userId, AdminEditProfileDto dto)
        {
            throw new NotImplementedException();
        }

    }
}
