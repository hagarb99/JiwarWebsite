using Jiwar.DTOs.PropertyComparisonDTO;
using Jiwar.Enum;
namespace Jiwar.Services.AI.Comparison
{
    public interface IPropertyComparisonAiService
    {
        Task<AiComparisonResultDTO> CompareAsync(List<PropertyComparisonDTO> propertiesDto, PropertyComparisonUserType userType);
    }
}
