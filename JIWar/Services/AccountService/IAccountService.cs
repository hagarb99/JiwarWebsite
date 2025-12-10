using GEWAR.Models;
using Jiwar.DTOs.AccountDTOs.EditProfileDtos;
using Jiwar.Helpers;
using Jiwar.Models;
using Jiwar.DTOs.AccountDTOs.EditProfileDtos;
using Jiwar.DTOs.AccountDTOs;

using System.Security.Claims;
using Jiwar.Account.DTOs;

namespace Jiwar.Account
{
    public interface IAccountService
    {
        //Task<UserResponseDTO> RegisterAsync(RegisterDto dto);
        //Task<UserResponseDTO> LoginAsync(LoginDto dto);
        //Task<ResultViewModel<string>> ChangePasswordAsync(ChangePasswordDto dto);
        //Task<ResultViewModel<string>> ForgetPasswordAsync(ForgetPasswordDto dto);
        //Task<ResultViewModel<string>> ResetPasswordAsync(ResetPasswordDto dto);
        //Task<ResultViewModel<string>> EditProfileAsync(EditProfileDto dto);
        Task<ResultViewModel<UserResponseDTO>> RegisterAsync(RegisterDto dto);
        Task<ResultViewModel<UserResponseDTO>> LoginAsync(LoginDto dto);
        Task<ResultViewModel<string>> ChangePasswordAsync(ClaimsPrincipal userClaims, ChangePasswordDto dto);
        Task<ResultViewModel<string>> ForgetPasswordAsync(ForgetPasswordDto dto);
        Task<ResultViewModel<string>> ResetPasswordAsync(ResetPasswordDto dto);
        Task<ResultViewModel<UserResponseDTO>> EditProfileAsync(ClaimsPrincipal userClaims, EditProfileDto dto);
        Task<bool> RoleExistsAsync(string roleName);
        Task AddUserToRoleAsync(User user, string roleName);
        Task AddPropertyOwnerAsync(PropertyOwner owner);
        Task AddInteriorDesignerAsync(InteriorDesigner designer);

        Task UpdateCustomerProfileAsync(Guid userId, CustomerEditProfileDto dto);
        Task UpdatePropertyOwnerProfileAsync(Guid userId, PropertyOwnerEditProfileDto dto);
        Task UpdateInteriorDesignerProfileAsync(Guid userId,InteriorDesignerEditProfileDto dto);
        Task UpdateAdminProfileAsync(Guid userId, AdminEditProfileDto dto);



    }
}
