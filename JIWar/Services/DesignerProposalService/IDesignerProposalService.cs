using Jiwar.DTOs.DesignDto;

namespace Jiwar.Services.DesignerProposalService
{
    public interface IDesignerProposalService
    {
        Task<ProposalDto> SendProposalAsync(string designerId, ProposalDto dto);
        Task<List<ProposalForOwnerDto>> GetProposalsForRequestAsync(int requestId);
        Task<ProposalDto> ChooseProposalAsync(int proposalId, string ownerId);
        Task<IEnumerable<ProposalDto>> GetProposalsForDesignerAsync(string designerId);

    }

}
