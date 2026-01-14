using GEWAR.Models;
namespace Jiwar.DTOs.DesignDto
{
  public class ProposalDto
{
    public int Id { get; set; }

    public int RequestID { get; set; }

    public int Status { get; set; }

    public decimal? PriceEstimate { get; set; }

    public string OfferDetails { get; set; }

    public string DesignerName { get; set; }

    public string DesignerEmail { get; set; }
}


}
