using GEWAR.Models;
using System.Text;

namespace Jiwar.Services.AI.Prompts.Renovations
{
    public static class RenovationContextBuilder
    {
        public static string Build(RenovationSimulation simulation)
        {
            var sb = new StringBuilder();

            sb.AppendLine("PROPERTY INFORMATION");
            sb.AppendLine($"PropertyId: {simulation.PropertyID}");
            sb.AppendLine($"Budget Range: {simulation.BudgetMin} - {simulation.BudgetMax}");

            if (!string.IsNullOrWhiteSpace(simulation.RenovationGoalsJson))
            {
                sb.AppendLine("USER GOALS:");
                sb.AppendLine(simulation.RenovationGoalsJson);
            }

            sb.AppendLine("RULES:");
            sb.AppendLine("- Respect the provided budget");
            sb.AppendLine("- Focus on value-increasing renovations");
            sb.AppendLine("- Output must follow the required JSON schema");

            return sb.ToString();
        }
    }
}
