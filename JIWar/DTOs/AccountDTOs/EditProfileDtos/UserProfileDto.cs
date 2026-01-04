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
    }

    public class InteriorDesignerDto
    {
        public string Specialty { get; set; }
    }
    public class UserProfileDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string ProfilePicURL { get; set; }
        public DateTime RegistrationDate { get; set; }
        public PropertyOwnerDto PropertyOwner { get; set; }
        public InteriorDesignerDto InteriorDesigner { get; set; }
    }

}
