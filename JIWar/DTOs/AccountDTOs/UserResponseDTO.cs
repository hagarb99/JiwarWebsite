using GEWAR.Models;

namespace Jiwar.Account
{
    public class UserResponseDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        //public UserTypeEnum UserType { get; set; }
        public string ProfilePicURL { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string? GoogleId { get; set; }
        public bool IsProfileCompleted { get; set; }


    }
}
