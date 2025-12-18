namespace Jiwar.DTOs.DesignDto
{
    public class DesignerProposalDto
    {
        public int Id { get; set; }
        public int DesignRequestID { get; set; }
        public virtual  DesignRequestDto DesignRequest { get; set; }
        public string DesignerID { get; set; }
        public decimal EstimatedCost { get; set; }
        public int EstimatedDays { get; set; }
        public string ProposalDescription { get; set; }
        public string SampleDesignURL { get; set; }
        public string Status { get; set; }
    }
}
