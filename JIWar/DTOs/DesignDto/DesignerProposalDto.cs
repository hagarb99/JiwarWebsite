using GEWAR.Models;
namespace Jiwar.DTOs.DesignDto
{
    public class DesignerProposalDto
    {
        public string Name { get; set; }
        public string InteriorDesignerEmail { get; set; }   // ناخد الإيميل من الـ navigation
        public int DesignRequestID { get; set; }
        public string Status { get; set; }
        public decimal EstimatedCost { get; set; }
        public int EstimatedDays { get; set; }
        public string ProposalDescription { get; set; }
        public string? SampleDesignURL { get; set; }
    }

}
