using Jiwar.DTOs.DesignDto;

namespace Jiwar.Services.DesignerProposalService
{
    public interface IDesignerProposalService
    {
        Task<DesignerProposalDto> SendProposalAsync(string designerId, DesignerProposalDto dto);
        Task<List<ProposalForOwnerDto>> GetProposalsForRequestAsync(int requestId);
        Task<DesignerProposalDto> ChooseProposalAsync(int proposalId, string ownerId);
        Task<IEnumerable<DesignerProposalDto>> GetProposalsForDesignerAsync(string designerId);

    }

}
