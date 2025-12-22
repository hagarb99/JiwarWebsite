using Jiwar.DTOs.ValuationDTOs;

namespace Jiwar.Services.ValuationService
{
    public interface IValuationHistoryService
    {
        Task SaveValuationAsync(ValuationSaveDTO dto, string userId);
        Task<IEnumerable<UserValuationListDTO>> GetMyValuationsAsync(string userId);
    }
}
