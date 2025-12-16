namespace Jiwar.DTOs.ProposalDto
{
    public class ProposalDto
    {
        public int Id { get; set; }
        public int RequestID { get; set; }
        public string DesignerID { get; set; }
        public string OfferDetails { get; set; }
        public decimal? PriceEstimate { get; set; }
        public string Status { get; set; }
    }

}
