namespace Jiwar.DTOs.AccountDTOs.EditProfileDtos
{
    public class AdminEditProfileDto : EditProfileBaseDto
    {
        // Admin-specific editable fields (usually minimal)
        public string? DisplayName { get; set; }
        // Do NOT allow role/permission changes here — handle separately

       



    }
}
