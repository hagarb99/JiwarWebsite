using System.ComponentModel.DataAnnotations;

namespace Jiwar.DTOs.AccountDTOs.EditProfileDtos
{
    public class EditProfileBaseDto
    {
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicURL { get; set; }
        public string? Phone { get; set; }
     
        public string? AvatarUrl { get; set; }

    }
}
