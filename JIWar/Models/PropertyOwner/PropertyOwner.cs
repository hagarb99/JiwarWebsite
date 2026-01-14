using GEWAR.Models;

namespace Jiwar.Models
{
    public class PropertyOwner
    {
        public virtual User Owneruser { get; set; } = null!;
        public string UserID { get; set; } = null!;
        public virtual ICollection<Property> Properties { get; set; } = new List<Property>();

        public PlanTypeEnum planTypeEnum { get; set; }

        public StatusEnum2 statusEnum2 { get; set; }
    }
}
