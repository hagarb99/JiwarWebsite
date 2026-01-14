using Jiwar.DTOs.DesignDto;

namespace Jiwar.Services.DesignRequestService
{
    public interface IDesignRequestService
    {
        Task<DesignRequestDto> CreateDesignRequestAsync(string userId, DesignRequestDto dto);
        Task<List<DesignRequestDto>> GetUserRequestsAsync(string userId);       // للـ Owner: My Requests
        Task<List<DesignRequestDto>> GetAvailableRequestsAsync();              // للـ Designers: Available
        Task<DesignRequestDto> GetRequestByIdAsync(int id);                    // تفاصيل طلب واحد
        Task UpdateRequestStatusAsync(int requestId, string status);           // يستخدمها proposal/design
        Task<WorkspaceDto> GetWorkspaceAsync(int requestId, string currentUserId = null);
        Task<List<Jiwar.DTOs.ChatDTOs.ConversationDTO>> GetUserConversationsAsync(string userId);
        Task MarkMessagesAsReadAsync(string userId, int propertyId);
        Task<int> GetTotalUnreadMessagesCountAsync(string userId);
    }
}
