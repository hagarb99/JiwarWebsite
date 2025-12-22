namespace Jiwar.DTOs
{
    public class SimulationResultDto
{
    public int SimulationId { get; set; }

    public decimal? BudgetMin { get; set; }
    public decimal? BudgetMax { get; set; }

    public List<string> Goals { get; set; }

    public List<UploadSimulationMediaDto> Medias { get; set; }
    public List<SimulationRecommendationDto> Recommendations { get; set; }
}

}