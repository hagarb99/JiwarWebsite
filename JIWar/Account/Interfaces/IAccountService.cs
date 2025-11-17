
using Jiwar.Helpers;
using Jiwar.Models;

namespace Jiwar.Account.DTOs
{
    public interface IAccountService
    {
        Task<UserResponseDTO> RegisterAsync(RegisterDto dto);
        Task<UserResponseDTO> LoginAsync(LoginDto dto);
        Task<ServiceResult> ChangePasswordAsync(ChangePasswordDto dto);
        Task<ServiceResult> ForgetPasswordAsync(ForgetPasswordDto dto);
        Task<ServiceResult> ResetPasswordAsync(ResetPasswordDto dto);
        Task<ServiceResult> EditProfileAsync(EditProfileDto dto);

    }
}
