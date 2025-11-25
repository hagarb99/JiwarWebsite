
using Jiwar.Helpers;
using Jiwar.Models;

namespace Jiwar.Account.DTOs
{
    public interface IAccountService
    {
        Task<UserResponseDTO> RegisterAsync(RegisterDto dto);
        Task<UserResponseDTO> LoginAsync(LoginDto dto);
        Task<ResultViewModel<string>> ChangePasswordAsync(ChangePasswordDto dto);
        Task<ResultViewModel<string>> ForgetPasswordAsync(ForgetPasswordDto dto);
        Task<ResultViewModel<string>> ResetPasswordAsync(ResetPasswordDto dto);
        Task<ResultViewModel<string>> EditProfileAsync(EditProfileDto dto);

    }
}
