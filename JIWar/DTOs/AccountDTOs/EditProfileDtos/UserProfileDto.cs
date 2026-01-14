namespace Jiwar.DTOs
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
    public class PropertyOwnerDto
    {
        public string PlanType { get; set; }
        public string Status { get; set; }
        public List<PropertyDto> Properties { get; set; } = new List<PropertyDto>();
        public string UserId { get; set; }
        public string Name { get; set; }
        public string ProfilePicURL { get; set; }
        public string Location { get; set; } // أضيفي هذا
        public string Bio { get; set; }      // أضيفي هذا
        public string Title { get; set; }
    }


    public class InteriorDesignerDto
    {
        public string Specialty { get; set; } // تأكدي إن الاسم ده هو اللي بتستخدميه أو غيريه لـ Specialization
        public string? Name { get; set; }      // أضيفي هذا
        public string? ProfilePicURL { get; set; } // أضيفي هذا
        public int? ExperienceYears { get; set; }  // أضيفي هذا
        public string? PortfolioURL { get; set; }
    }
    public class UserProfileDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string? ProfilePicURL { get; set; }
        public DateTime RegistrationDate { get; set; }
        public PropertyOwnerDto PropertyOwner { get; set; }
        public InteriorDesignerDto InteriorDesigner { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Location { get; set; }
        public string? Bio { get; set; }

        public void Normalize()
        {
            Bio ??= "No bio information provided yet.";
            
            if (InteriorDesigner != null)
            {
                InteriorDesigner.Specializations ??= new List<string>();
                InteriorDesigner.Certifications ??= new List<string>();
                InteriorDesigner.Specialty ??= "Not specified";
            }
        }
    }

}
