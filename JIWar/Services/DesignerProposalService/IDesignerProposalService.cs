using Jiwar.DTOs.DesignDto;

namespace Jiwar.Services.DesignerProposalService
{
    public interface IDesignerProposalService
    {
        Task<ProposalDto> SendProposalAsync(string designerId, ProposalDto dto);
        Task<List<ProposalForOwnerDto>> GetProposalsForRequestAsync(int requestId);
        Task<List<ProposalForOwnerDto>> ChooseProposalAsync(int proposalId, string ownerId);
        Task<IEnumerable<ProposalDto>> GetProposalsForDesignerAsync(string designerId);
        Task<bool> DeliverProposalAsync(int proposalId, string designerId, string notes);

    }

}
