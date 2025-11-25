using GEWAR.Models;

namespace Jiwar.Models
{
    public class PropertyOwner
    {
        public User Owneruser { get; set; }
        public int Id { get; set; }              // Primary Key مهم لازم يكون موجود

        public string UserID { get; set; }       // FK للـ Identity User
        public virtual ICollection<Property> Properties { get; set; } = new List<Property>();

        public PlanTypeEnum planTypeEnum { get; set; }

        public StatusEnum2 statusEnum2 { get; set; }
    }
}
