namespace Jiwar.DTOs.AccountDTOs.EditProfileDtos
{
    public class InteriorDesignerEditProfileDto : EditProfileBaseDto
    {


        public string? PortfolioUrl { get; set; }
        public int? YearsOfExperience { get; set; }
        public string? Specialization { get; set; }
        public string? Specializations { get; set; }
        public string? Certifications { get; set; }
    }
}
