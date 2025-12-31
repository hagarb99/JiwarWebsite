using GEWAR.Models.Jiwar.Enum;

namespace GEWAR.Models
{
   
   public class RenovationSimulation : BaseModel
{
    public int? PropertyID { get; set; }
    public string UserID { get; set; }

    public decimal? BudgetMin { get; set; }
    public decimal? BudgetMax { get; set; }

    // goals selected by user
    public string? RenovationGoalsJson { get; set; } // JSON

    public SimulationStatusEnum Status { get; set; } // Draft / Completed

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public virtual Property Property { get; set; }
    public virtual ICollection<SimulationRecommendation> Recommendations { get; set; }

    public virtual ICollection<SimulationMedia> Medias { get; set; } = new List<SimulationMedia>();

}


}