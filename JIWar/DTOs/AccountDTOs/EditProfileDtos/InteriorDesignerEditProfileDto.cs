namespace Jiwar.DTOs.AccountDTOs.EditProfileDtos
{
    public class InteriorDesignerEditProfileDto : EditProfileBaseDto
    {
       // Designer-specific editable fields
        public string? PortfolioUrl { get; set; }
        public int? YearsOfExperience { get; set; }
        public string? Specialization { get; set; }
    }
}