namespace Jiwar.DTOs.CreateProposalDto
{
    public class CreateProposalDto
    {
        public int RequestID { get; set; }
        public string DesignerID { get; set; }
        public string OfferDetails { get; set; }
        public decimal? PriceEstimate { get; set; }
    }

}
