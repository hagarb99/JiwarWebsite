
using AutoMapper;
using GEWAR.Models;
using Jiwar.Account.DTOs;
using Jiwar.Controllers;
using Jiwar.DTOs;
using Jiwar.DTOs.AccountDTOs.EditProfileDtos;
using Jiwar.Helpers;
using Jiwar.Models;
using Jiwar.Repositories;
using Jiwar.Services.GoogleService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper mapper;
        public AccountService(
            IAccountRepository repo,
            TokenService tokenService ,
            GoogleAuthService _googleAuthService,
            UserManager<User> _userManager,
            IMapper mapper)
        {
            this.repo = repo;
            _tokenService = tokenService;
            this._googleAuthService = _googleAuthService;
            this._userManager = _userManager;
            this.mapper = mapper;
        }
        public async Task<ResultViewModel<UserResponseDTO>> RegisterAsync(RegisterDto dto)
        {
            var user = mapper.Map<User>(dto);
            var result = await repo.CreateUserAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                return ResultViewModel<UserResponseDTO>.Fail(
         string.Join("; ", result.Errors.Select(e => e.Description))
     );

            }

            await repo.AddUserToRoleAsync(user, dto.Role);

            if (dto.Role == "PropertyOwner")
            {
                var owner = new PropertyOwner { UserID = user.Id };
                await repo.AddPropertyOwnerAsync(owner);
            }
            else if (dto.Role == "InteriorDesigner")
            {
                var designer = new InteriorDesigner { InteriorDesignerID = user.Id };
                await repo.AddInteriorDesignerAsync(designer);
            }

            return ResultViewModel<UserResponseDTO>.Ok(
                "User registered successfully.",
                 mapper.Map<UserResponseDTO>(user)
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


            var userDto = mapper.Map<UserResponseDTO>(user);
            userDto.Token = token;
            userDto.IsProfileCompleted = isProfileCompleted;

            return ResultViewModel<UserResponseDTO>.Ok("Login successful.", userDto);
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

            mapper.Map(dto, user);

            var result = await repo.UpdateUserAsync(user);

            if (!result.Succeeded)
                return ResultViewModel<UserResponseDTO>.Fail("Failed to update profile.");

            return ResultViewModel<UserResponseDTO>.Ok(
                "Profile updated successfully.",
                mapper.Map<UserResponseDTO>(user)
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

        //public async Task<ResultViewModel<UserResponseDTO>> GoogleSignInAsync(string idToken)
        //{
        //    var payload = await _googleAuthService.VerifyGoogleTokenAsync(idToken);
        //    if (payload == null)
        //        return ResultViewModel<UserResponseDTO>.Fail("Invalid Google token");

        //    var user = await _userManager.FindByEmailAsync(payload.Email);
        //    if (user == null)
        //    {
        //        user = new User
        //        {
        //            UserName = payload.Email,
        //            Email = payload.Email,
        //            GoogleId = payload.Subject,
        //            Name = payload.Name,
        //            ProfilePicURL = payload.Picture,
        //            RegistrationDate = DateTime.UtcNow,
        //            Role = "Customer"
        //        };

        //        var result = await _userManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            // Optional: Disable password requirement explicitly
        //            await _userManager.RemovePasswordAsync(user);  // Removes any password requirement
        //        }
        //        //if (!result.Succeeded)
        //        //    return ResultViewModel<UserResponseDTO>.Fail(string.Join("; ", result.Errors.Select(e => e.Description)));
        //    }

        //    var token = await _tokenService.CreateTokenAsync(user);

        //    var userDto = mapper.Map<UserResponseDTO>(user);
        //    userDto.Token = token;

        //    return ResultViewModel<UserResponseDTO>.Ok("Login successful", userDto);
        //}
        public async Task<ResultViewModel<UserResponseDTO>> GoogleSignInAsync(string idToken)
        {
            var payload = await _googleAuthService.VerifyGoogleTokenAsync(idToken);
            if (payload == null)
                return ResultViewModel<UserResponseDTO>.Fail("Invalid Google token");

            var user = await _userManager.FindByEmailAsync(payload.Email);

            if (user == null)
            {
                // Create new user for Google login (no password)
                user = new User
                {
                    UserName = payload.Email,
                    Email = payload.Email,
                    GoogleId = payload.Subject,
                    Name = payload.Name,
                    ProfilePicURL = payload.Picture,
                    RegistrationDate = DateTime.UtcNow,
                    Role = "Customer",
                };

                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                {
                    return ResultViewModel<UserResponseDTO>.Fail(
                        string.Join("; ", createResult.Errors.Select(e => e.Description)));
                }

                // Remove password requirement since this is external login
                await _userManager.RemovePasswordAsync(user);

                // Optional: Add external login info (recommended for better Identity support)
                await _userManager.AddLoginAsync(user, new UserLoginInfo("Google", payload.Subject, "Google"));
            }
            else
            {
                // Existing user – update Google data if changed
                user.GoogleId = payload.Subject;
                user.Name = payload.Name;
                user.ProfilePicURL = payload.Picture;
                await _userManager.UpdateAsync(user);
            }

            // Generate JWT token
            var token = await _tokenService.CreateTokenAsync(user);

            // Map to response DTO
            var userDto = mapper.Map<UserResponseDTO>(user);
            userDto.Token = token;

            // Optional: Set IsProfileCompleted if needed
            // userDto.IsProfileCompleted = ...

            return ResultViewModel<UserResponseDTO>.Ok("Login successful", userDto);
        }

        public Task UpdateCustomerProfileAsync(string userId, CustomerEditProfileDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task UpdatePropertyOwnerProfileAsync(string userId, PropertyOwnerEditProfileDto dto)
        {
            var owner = await repo.GetPropertyOwnerByUserIdAsync(userId);

            if (owner == null)
                throw new Exception("PropertyOwner not found.");

            mapper.Map(dto, owner);

            await repo.UpdatePropertyOwnerAsync(owner);
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

        public async Task<UserProfileDto> GetUserProfileAsync(string userId)
        {
            var user = await repo.GetUserByIdAsync(userId);
            if (user == null) return null;

            return mapper.Map<UserProfileDto>(user);
        }

    }
}
