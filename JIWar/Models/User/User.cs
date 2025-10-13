using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEWAR.Models
{
   public partial class User : BaseModel
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string ProfilePicURL { get; set; }
        public string ProfilePicName { get; set; }
        public string UserType { get; set; }
           public DateTime RegistrationDate { get; set; }
        public static bool IsValidUserType(string userType)
        {
            return Enum.TryParse<UserTypeEnum>(userType, true, out var result) &&
                   Enum.IsDefined(typeof(UserTypeEnum), result);
        }





    }
}
