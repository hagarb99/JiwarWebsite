namespace Jiwar.DTOs.AdminAnalytics
{
    public class AdminUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime RegistrationDate { get; set; }
        
        public bool IsActive { get; set; }
    }

}
