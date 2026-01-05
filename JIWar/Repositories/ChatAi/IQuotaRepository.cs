using Jiwar.Models.ChatAi;

namespace Jiwar.Repositories.ChatAi
{
    public interface IQuotaRepository
    {
        Task<UserChatQuota?> GetByUserIdAsync(string userId);
        Task AddAsync(UserChatQuota quota);
        Task UpdateAsync(UserChatQuota quota);

    }
}
