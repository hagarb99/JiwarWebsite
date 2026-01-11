using GEWAR.Models;
using Jiwar.Models.ChatAi;
using Jiwar.Services.AI;
using Jiwar.Services.AI.Chat;
using Jiwar.Services.AI.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Jiwar.Controllers
{
    [ApiController]
    [Route("api/ai-chat")]
    public class RenovationChatController : ControllerBase
    {
        private readonly IAiChatService _chatService;
        private readonly IAiService _aiService; // للصور المولدة من AI


        public RenovationChatController(IAiChatService chatService , IAiService aiService)
        {
            _chatService = chatService;
            _aiService = aiService;

        }

        [HttpPost("start")]
        public async Task<IActionResult> Start(int simulationId)
        {
            var message = await _chatService.StartChatAsync(simulationId);
            return Ok(new { message });
        }

        //private IActionResult Ok(object value)
        //{
        //    throw new NotImplementedException();
        //}

        [HttpPost("send")]
        public async Task<IActionResult> Send(
            int simulationId,
            string userId,
            [FromBody] string UserMessage)
        {
            var response = await _chatService.SendMessageAsync(simulationId,userId ,UserMessage);
            return Ok(new { response });
        }


        // =====================
        // Upload Image
        // =====================
        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(int simulationId, [FromForm] IFormFile image)
        {
            var userId = User.Claims.FirstOrDefault(c=>c.Type ==ClaimTypes.NameIdentifier).Value; 
            if (image == null || image.Length == 0)
                return BadRequest("No image uploaded.");

            var fileName = $"{Guid.NewGuid()}_{image.FileName}";
            var path = Path.Combine("wwwroot/uploads/renovation", fileName);

            using var stream = new FileStream(path, FileMode.Create);
            await image.CopyToAsync(stream);

            var chatMessage = new SimulationChatMessage
            {
                UserId = userId,
                RenovationSimulationID = simulationId,
                Sender = ChatSenderEnum.User,
                MessageType = ChatMessageTypeEnum.Image,
                Content = $"/uploads/renovation/{fileName}"
            };

            // نخزن الرسالة عبر service بدل repo مباشرة
            await _chatService.SaveMessageAsync(chatMessage);


            // we will add logic here


            return Ok(new { fileUrl = chatMessage.Content });
        }


        // =====================
        // Send Voice
        // =====================
        [HttpPost("send-voice")]
        public async Task<IActionResult> SendVoice(int simulationId, [FromForm] IFormFile voiceFile)
        {
            if (voiceFile == null || voiceFile.Length == 0)
                return BadRequest("No voice file uploaded.");

            var fileName = $"{Guid.NewGuid()}_{voiceFile.FileName}";
            var path = Path.Combine("wwwroot/uploads", fileName);

            using var stream = new FileStream(path, FileMode.Create);
            await voiceFile.CopyToAsync(stream);

            var chatMessage = new SimulationChatMessage
            {
                RenovationSimulationID = simulationId,
                Sender = ChatSenderEnum.User,
                MessageType = ChatMessageTypeEnum.Voice,
                Content = $"/uploads/{fileName}"
            };

            await _chatService.SaveMessageAsync(chatMessage);

            return Ok(new { fileUrl = chatMessage.Content });
        }

        // =====================
        // Generate Image via AI
        // =====================
        [HttpPost("generate-image")]
        public async Task<IActionResult> GenerateImage(int simulationId, [FromBody] string prompt)
        {
            var urls = await _aiService.GenerateImagesAsync(prompt, AiModelEnum.DallE3);

            var chatMessage = new SimulationChatMessage
            {
                RenovationSimulationID = simulationId,
                Sender = ChatSenderEnum.AI,
                MessageType = ChatMessageTypeEnum.Image,
                Content = urls.FirstOrDefault() ?? ""
            };

            await _chatService.SaveMessageAsync(chatMessage);

            return Ok(new { imageUrl = chatMessage.Content });
        }

    }
}
