namespace Jiwar.Account
{
    public interface IAccountService
    {
        Task<UserResponseDTO> RegisterAsync(RegisterDto dto);
        //Task<UserResponseDTO> LoginAsync();
    }
}
