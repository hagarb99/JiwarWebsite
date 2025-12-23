using GEWAR.Models;

namespace Jiwar.Models
{
    public class DesignerProposal : BaseModel
    {





        public int DesignRequestID { get; set; }
        public virtual DesignRequest DesignRequest { get; set; } = null!;

        public string DesignerID { get; set; }
        public virtual InteriorDesigner Designer { get; set; }

        public decimal EstimatedCost { get; set; }

        public int EstimatedDays { get; set; }

        public string ProposalDescription { get; set; }

        public string SampleDesignURL { get; set; }

        public string Status { get; set; }

    }


}
