using GEWAR.Models;
using System;

namespace Jiwar.Models
{
    public class DesignerReview : BaseModel
    {
        public string DesignerID { get; set; } // Designer being reviewed
        public string PropertyOwnerID { get; set; } // Reviewer
        public int ProposalID { get; set; }
        public int DesignRequestID { get; set; }
        public int Rating { get; set; } // 1-5 stars
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public virtual User Designer { get; set; }
        public virtual User PropertyOwner { get; set; }
        public virtual DesignerProposal Proposal { get; set; }
        public virtual DesignRequest DesignRequest { get; set; }
    }
}
