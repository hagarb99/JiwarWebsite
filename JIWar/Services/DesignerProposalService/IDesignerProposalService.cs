using Jiwar.DTOs.DesignDto;

namespace Jiwar.Services.DesignerProposalService
{
    public interface IDesignerProposalService
    {
        Task<DesignerProposalDto> SendProposalAsync(string designerId, DesignerProposalDto dto);
        Task<List<DesignerProposalDto>> GetProposalsForRequestAsync(int requestId);
        Task<List<DesignerProposalDto>> GetProposalsForDesignerAsync(string designerId);
        Task<DesignerProposalDto> ChooseProposalAsync(int proposalId, string ownerId);
    }

}
