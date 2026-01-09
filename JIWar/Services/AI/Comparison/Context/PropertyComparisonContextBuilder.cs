using System.Text.Json;
using Jiwar.DTOs.PropertyComparisonDTO;
using Jiwar.Enum;

namespace Jiwar.Services.AI.Comparison.Context
{
    public static class PropertyComparisonContextBuilder
    {
        public static string Build(List<PropertyComparisonDTO> properties, PropertyComparisonUserType userType)
        {
            // بنجهز object يحتوي على تعليمات اليوزر والعقارات
            var context = new
            {
                UserTypeInstruction = GetUserTypeInstruction(userType),
                Properties = properties.Select(p => new
                {
                    p.PropertyID,
                    p.Title,
                    p.City,
                    p.Address,
                    p.PropertyType,
                    Status = p.Status.ToString(),
                    p.Price,
                    Area_sqm = p.Area_sqm ?? 0,
                    NumBedrooms = p.NumBedrooms ?? 0,
                    NumBathrooms = p.NumBathrooms ?? 0,
                    Features = p.Features ?? new List<string>()
                }).ToList()
            };

            // نرجع JSON string صالح للـ AI
            return JsonSerializer.Serialize(context, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }

        private static string GetUserTypeInstruction(PropertyComparisonUserType type)
        {
            return type switch
            {
                PropertyComparisonUserType.Investor =>
                    "Focus on ROI, rental demand, price per sqm, and long-term value.",

                PropertyComparisonUserType.Family =>
                    "Focus on comfort, space, number of rooms, and family suitability.",

                PropertyComparisonUserType.BudgetBuyer =>
                    "Focus on lowest price, value for money, and essential needs.",

                _ => string.Empty
            };
        }
    }
}
