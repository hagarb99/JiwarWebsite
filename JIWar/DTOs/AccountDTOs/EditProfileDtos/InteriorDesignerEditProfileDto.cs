namespace Jiwar.DTOs.AccountDTOs.EditProfileDtos
{
    public class InteriorDesignerEditProfileDto : EditProfileBaseDto
    {


        public string? Website { get; set; } // Ant Gravity يرسل هذا الآن
        public List<string>? Specializations { get; set; } // يرسل مصفوفة
        public int? YearsOfExperience { get; set; }

        // سنحتفظ بهذين الحقلين لضمان عدم حدوث Error أثناء الـ Mapping
        public string? PortfolioUrl { get; set; }
        public string? Specialization { get; set; }
    }
}
