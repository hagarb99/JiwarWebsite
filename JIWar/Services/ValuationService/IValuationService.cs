using Jiwar.DTOs.ValuationDTOs;

namespace Jiwar.Services.ValuationService
{
    public interface IValuationService
    {
        ValuationResultDTO CalculateValuation(ValuationRequestDTO request);
    }
}
