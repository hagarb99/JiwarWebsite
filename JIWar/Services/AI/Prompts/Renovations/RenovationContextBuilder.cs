using GEWAR.Models;
using Jiwar.Models;
using System.Text;

namespace Jiwar.Services.AI.Prompts.Renovations
{
    public static class RenovationContextBuilder
    {
        public static string Build(RenovationSimulation simulation , SimulationDetails details)
        {
            var sb = new StringBuilder();

            if (simulation.PropertyID == null)
            {
                sb.AppendLine("STANDALONE PROPERTY DETAILS:");
                sb.AppendLine($"Size: {details.Size}");
                sb.AppendLine($"Rooms: {details.Rooms}");
                sb.AppendLine($"Bathrooms: {details.Bathrooms}");
                sb.AppendLine($"Condition: {details.Condition}");
            }
            else
            {
                sb.AppendLine("EXISTING PROPERTY INFORMATION:");
                sb.AppendLine($"PropertyId: {simulation.PropertyID}");
                sb.AppendLine($"Budget: {simulation.BudgetMin} - {simulation.BudgetMax}");
            }

            return sb.ToString();
        }
    }
}
