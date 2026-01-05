using System.ComponentModel.DataAnnotations;

namespace Jiwar.DTOs.AccountDTOs.EditProfileDtos
{
    public class EditProfileBaseDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfilePicURL { get; set; }     
        public string? Location { get; set; }
        public string? Bio { get; set; }

    }
}
