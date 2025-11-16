
using Jiwar.Models;

namespace Jiwar.Account.DTOs
{
    public interface IAccountService
    {
        Task<UserResponseDTO> RegisterAsync(RegisterDto dto);
        Task<UserResponseDTO> LoginAsync(LoginDto dto);
        Task ChangePasswordAsync(ChangePasswordDto dto);

    }
}
