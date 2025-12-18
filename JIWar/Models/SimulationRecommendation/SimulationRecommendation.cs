namespace GEWAR.Models
{
    //api result from model ai
    public class SimulationRecommendation : BaseModel
{
    public int RenovationSimulationID { get; set; }

    public RecommendationCategoryEnum Category { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public RecommendationSeverityEnum Severity { get; set; }

    public bool IsAIGenerated { get; set; }

    // Navigation
    public virtual RenovationSimulation RenovationSimulation { get; set; }
}

}