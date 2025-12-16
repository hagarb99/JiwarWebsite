using GEWAR.Models;
using System.ComponentModel.DataAnnotations;

namespace Jiwar.Account.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression("Customer|PropertyOwner|InteriorDesigner|Admin")]
        public string Role { get; set; }
    }
}
