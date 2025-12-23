using Jiwar.Services.AI.Enums;

namespace Jiwar.Services.AI
{
    public class AiRequestContext
    {
        public AiModelEnum Model { get; set; }
        public string Purpose { get; set; }
        public bool RequiresImage { get; set; }
    }
}
