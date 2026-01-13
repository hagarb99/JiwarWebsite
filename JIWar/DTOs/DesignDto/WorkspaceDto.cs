using Jiwar.DTOs.DesignDto;
using Jiwar.Models;
using System;

namespace Jiwar.DTOs.DesignDto
{
    public class WorkspaceDto
    {
        public DesignRequestDto DesignRequest { get; set; }
        public AcceptedProposalDto AcceptedProposal { get; set; }
        public List<Jiwar.DTOs.ChatDTOs.ChatMessageDTO> ChatHistory { get; set; } = new();
        public bool HasDelivered { get; set; }
        public bool HasReviewed { get; set; }
    }

    public class AcceptedProposalDto
    {
        public int Id { get; set; }
        public string DesignerId { get; set; }
        public string DesignerName { get; set; }
        public decimal EstimatedCost { get; set; }
        public int EstimatedDays { get; set; }
        public int Status { get; set; }
        public DateTime? DeliveredAt { get; set; }
    }
}
