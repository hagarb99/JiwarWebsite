using GEWAR.Models;
using Jiwar.Models;

namespace Jiwar.Services.AI.Chat.Context
{
    public class RenovationChatContextBuilder
    {
        public static string Build(
        RenovationSimulation simulation,
        SimulationDetails details,
         Property? property,
        IEnumerable<SimulationRecommendation> recommendations)
        {
            var size = details?.Size
        ?? property?.Area_sqm
        ?? 0;

            var rooms = details?.Rooms
                ?? property?.NumBedrooms
                ?? 0;

            var bathrooms = details?.Bathrooms
                ?? property?.NumBathrooms
                ?? 0;

            var condition = details?.Condition?.ToString()
                ?? property?.Condition?.ToString()
                ?? "Unknown";

            return $"""
Renovation Project Summary:

Area: {size} sqm
Rooms: {rooms}
Bathrooms: {bathrooms}
Condition: {condition}

Budget: {simulation.BudgetMin} - {simulation.BudgetMax}

Key Recommendations:
{(recommendations != null && recommendations.Any()
            ? string.Join("\n", recommendations.Select(r => "- " + r.Title))
            : "- No recommendations available")}
""";
        }
    }
}
