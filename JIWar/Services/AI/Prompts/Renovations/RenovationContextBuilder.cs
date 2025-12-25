using GEWAR.Models;

namespace Jiwar.Services.AI.Prompts.Renovations
{
    public static class RenovationContextBuilder
    {
        public static string Build(RenovationSimulation simulation)
        {
            return $"""
        Property ID: {simulation.PropertyID}
        Budget: {simulation.BudgetMin} - {simulation.BudgetMax}
        Goals: {simulation.RenovationGoalsJson}
        """;
        }
    }
}
