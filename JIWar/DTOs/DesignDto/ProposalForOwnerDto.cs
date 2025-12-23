using GEWAR.Models;
namespace Jiwar.DTOs.DesignDto
{
    public class ProposalForOwnerDto
    {
        public int Id { get; set; }

        public decimal EstimatedCost { get; set; }
        public int EstimatedDays { get; set; }
        public string ProposalDescription { get; set; }

        public string DesignerName { get; set; }
        public virtual InteriorDesigner InteriorDesigner { get; set; }

        public string DesignerEmail { get; set; }
    }
}
