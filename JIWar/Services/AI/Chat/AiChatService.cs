using Jiwar.Models.ChatAi;
using Jiwar.Repositories.ChatAi;
using Jiwar.Repositories.SimulationChatAI;
using Jiwar.Services.AI.Chat.Context;
using Jiwar.Services.AI.Chat.Prompts;
using Jiwar.Services.AI.Enums;

namespace Jiwar.Services.AI.Chat
{
    public class AiChatService : IAiChatService
    {
        private readonly IAiService _aiService;
        private readonly IRenovationSimulationRepository _simulationRepo;
        private readonly ISimulationChatRepository _chatRepo;
        private readonly IQuotaRepository _quotaRepo;

        public AiChatService(
     IAiService aiService,
     IRenovationSimulationRepository simulationRepo,
     ISimulationChatRepository chatRepo,
     IQuotaRepository quotaRepo
 )
        {
            _aiService = aiService;
            _simulationRepo = simulationRepo;
            _chatRepo = chatRepo;
            _quotaRepo = quotaRepo;

        }

        //  أول رسالة من الـ AI
        public async Task<string> StartChatAsync(int simulationId)
        {
            var simulation = await _simulationRepo.GetWithResultsAsync(simulationId)
                ?? throw new Exception("Simulation not found");

            var context = RenovationChatContextBuilder.Build(
                simulation,
                simulation.Details,
                simulation.Property,
                simulation.Recommendations);

            var aiResponse = await _aiService.SendAsync(
                RenovationChatSystemPrompt.Prompt,
                new AiRequestContext
                {
                    Purpose = context
                });

            await _chatRepo.AddAsync(new SimulationChatMessage
            {
                RenovationSimulationID = simulationId,
                Sender = ChatSenderEnum.AI,
                MessageType = ChatMessageTypeEnum.Text,
                Content = aiResponse
            });

            return aiResponse;
        }




        private const int MAX_MESSAGES = 50;

        private async Task CheckAndConsumeQuotaAsync(string userId)
        {
            var quota = await _quotaRepo.GetByUserIdAsync(userId);

            // أول مرة للمستخدم
            if (quota == null)
            {
                quota = new UserChatQuota
                {
                    UserId = userId,
                    RemainingMessages = MAX_MESSAGES
                };

                await _quotaRepo.AddAsync(quota);
            }

            if (quota.RemainingMessages <= 0)
                throw new Exception("Chat quota exceeded");

            quota.RemainingMessages--;
            await _quotaRepo.UpdateAsync(quota);
        }


        // رسالة من المستخدم + رد AI
        public async Task<string> SendMessageAsync(
               int simulationId,
               string userId,
               string userMessage)
        {
            await CheckAndConsumeQuotaAsync(userId);

            await _chatRepo.AddAsync(new SimulationChatMessage
            {
                RenovationSimulationID = simulationId,
                UserId = userId,
                Sender = ChatSenderEnum.User,
                MessageType = ChatMessageTypeEnum.Text,
                Content = userMessage
            });

            var history = await _chatRepo.GetBySimulationIdAsync(simulationId);

            var messagesContext = string.Join("\n",
                history.Select(m => $"{m.Sender}: {m.Content}"));

            var aiResponse = await _aiService.SendAsync(
                RenovationChatSystemPrompt.Prompt,
                new AiRequestContext
                {
                    Purpose = messagesContext
                });

            await _chatRepo.AddAsync(new SimulationChatMessage
            {
                RenovationSimulationID = simulationId,
                UserId = userId,
                Sender = ChatSenderEnum.AI,
                MessageType = ChatMessageTypeEnum.Text,
                Content = aiResponse
            });

            return aiResponse;
        }


        public async Task SaveMessageAsync(SimulationChatMessage message)
        {
            await _chatRepo.AddAsync(message);
        }
        public async Task<List<SimulationChatMessage>> GetUserMessagesAsync(string userId)
        {
            return await _chatRepo.GetByUserIdAsync(userId);
        }

        
    }
}