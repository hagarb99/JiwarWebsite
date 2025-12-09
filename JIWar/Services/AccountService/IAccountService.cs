
using GEWAR.Models;
using Jiwar.Helpers;
using Jiwar.Models;
using System.Security.Claims;

namespace Jiwar.Account.DTOs
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


    }
}
