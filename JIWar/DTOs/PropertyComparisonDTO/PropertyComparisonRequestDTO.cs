using Jiwar.Enum;

namespace Jiwar.DTOs.PropertyComparisonDTO
{
    public class PropertyComparisonRequestDTO
    {
        public List<int> PropertyIds { get; set; } = new();
        public PropertyComparisonUserType UserType { get; set; }
    }
}
