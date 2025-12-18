namespace Jiwar.DTOs
{
    public class SimulationGoalsDto
{
    public List<string> Goals { get; set; } = new();
    public decimal? BudgetMin { get; set; }
    public decimal? BudgetMax { get; set; }
    public string? Notes { get; set; }
}

}
