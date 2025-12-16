namespace Jiwar.DTOs.AccountDTOs.EditProfileDtos
{
    public class CustomerEditProfileDto : EditProfileBaseDto
    {
       
            public string? PreferredContactMethod { get; set; } // "Phone" أو "Email"
            public string? DefaultBillingAddress { get; set; }
     
    }

}

