using System.ComponentModel.DataAnnotations;

namespace Jiwar.Account.DTOs
{
    public class EditProfileDto
    {
        [Required]
        public string UserId { get; set; } 

        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicURL { get; set; }
    }
}
