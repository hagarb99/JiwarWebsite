using GEWAR.Models;

namespace Jiwar.Models.ChatAi
{
    public class UserChatQuota : BaseModel
    {
        public string UserId { get; set; } = string.Empty;
        public int RemainingMessages { get; set; }
        public DateTime LastReset { get; set; } = DateTime.UtcNow;
    }
}