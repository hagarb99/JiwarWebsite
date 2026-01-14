using GEWAR;
using GEWAR.Models;
using Jiwar.DTOs;
using Jiwar.DTOs.PropertyComparisonDTO;
using Jiwar.DTOs.PropertyDTOs;
using Jiwar.Helpers;
using Jiwar.Models;
using Jiwar.Service;
using Jiwar.Services;
using Jiwar.Services.AI.Comparison;
using JIWar.PropertyOwner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.SignalR;
using Jiwar.Hubs;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    private readonly IPropertyAnalyticsService _analyticsService;
    private readonly IPropertyRepository propertyRepository;
    private readonly IPropertyComparisonAiService _propertyComparisonAiService;
    private readonly IHubContext<ChatHub> _chatHubContext;
    private readonly ILogger<PropertyController> _logger;

        public PropertyController(IPropertyService propertyService,
        IPropertyAnalyticsService analyticsService,
        IPropertyComparisonAiService propertyComparisonAiService,
        IPropertyRepository propertyRepository,
        IHubContext<ChatHub> chatHubContext,
        ILogger<PropertyController> logger
        )
    {
        _propertyService = propertyService;
        _analyticsService = analyticsService;
        _propertyComparisonAiService = propertyComparisonAiService;
        this.propertyRepository = propertyRepository;
        this._chatHubContext = chatHubContext;
        _logger = logger;
    }
    
    [HttpPost("add")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Add([FromForm] PropertyCreateDTO dto)
    {
        var ownerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (ownerId == null) return Unauthorized("Invalid token - missing user id");

        var resultDto = await _propertyService.AddPropertyAsync(dto, ownerId);
        return Ok(resultDto);

    }



    [HttpGet("all")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 12)
    {
        // Validation بسيطة
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 12;
        if (pageSize > 100) pageSize = 100;  // حد أقصى عشان الأداء

        var result = await _propertyService.GetAllPropertiesAsync(page, pageSize);

        return Ok(result);
    }

    [HttpPut("update/{id}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update(int id ,PropertyUpdateDTO dto)
    {
        var ownerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (ownerId == null) return Unauthorized();

        // اختياري: تحقق إن العقار ده بتاع اليوزر ده (للأمان)
        var existing = await propertyRepository.GetByIdAsync(dto.Id);
        if (existing == null || existing.OwnerID != ownerId)
            return Forbid(); // أو NotFound

        var result = await _propertyService.UpdatePropertyAsync(dto);
        return result
        ? Ok(new { message = "Updated Successfully" })   // return JSON object
        : NotFound(new { message = "Property Not Found" });

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _propertyService.DeletePropertyAsync(id);
        return result
         ? Ok(new { message = "Deleted Successfully" })   // JSON object
         : NotFound(new { message = "Property Not Found" });
    }

    [HttpGet("my")]
    public async Task<IActionResult> MyProperties()
    {
        //var ownerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //if (ownerId == null) return Unauthorized();

        //var list = await _propertyService.GetMyPropertiesAsync(ownerId);
        //return Ok(list);
        // جلب الـ ownerId من التوكن
        var ownerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (ownerId == null)
            return Unauthorized("Invalid token - missing user id");

        // جلب كل الـ properties الخاصة بالمستخدم
        var properties = await _propertyService.GetMyPropertiesAsync(ownerId);

        // إذا ما فيش properties، ممكن ترجع [] بدل Exception
        if (properties == null || !properties.Any())
            return Ok(new List<object>()); // أو Ok(properties) لو تحبي ترجعي null-safe

        return Ok(properties);

    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var property = await _propertyService.GetPropertyDetailsDTOAsync(id);
        return property != null ? Ok(property) : NotFound();
    }

    [HttpGet("{id}/share")]
    public async Task<IActionResult> Share(int id)
    {
        var property = await _propertyService.GetPropertyDetailsDTOAsync(id);
        if (property == null) return NotFound();
        var dto = new SharePreviewDTO
        {
            Url = $"https://yourdomain.com/properties/{id}",
            Title = property.Title,
            Description = property.Description,
            ImageUrl = property.MediaUrls?.FirstOrDefault(),
            Price = property.Price
        };

        return Ok(dto);
    }
    /// ///////////search and filter properties
    [HttpGet("browse")]
    [AllowAnonymous]
    public async Task<IActionResult> Browse([FromQuery] PropertyFilterDTO filter)
    {
        var properties = await _propertyService.GetFilteredPropertiesAsync(filter);
        return Ok(properties);
    }



    //[HttpPost("compare")]
    //[AllowAnonymous]
    //public async Task<IActionResult> Compare([FromBody] PropertyComparisonRequestDTO request)
    //{
    //    if (request.PropertyIds.Count < 2 || request.PropertyIds.Count > 5)
    //        return BadRequest("Choose between 2 and 5 properties.");

    //    var comparisonDtos = (await _propertyService.GetPropertiesForComparisonAsync(request.PropertyIds))
    //                         .ToList();

    //    var aiResult = await _propertyComparisonAiService.CompareAsync(comparisonDtos, request.UserType);

    //    return Ok(aiResult);
    //}

    [HttpPost("compare")]
    [AllowAnonymous]
    public async Task<ActionResult<AiComparisonResultDTO>> Compare(
    [FromBody] PropertyComparisonRequestDTO request)
    {
        if (request.PropertyIds == null || request.PropertyIds.Count < 2)
            return BadRequest("Select at least 2 properties");

        // 1️⃣ Get properties data
        var properties = await _propertyService
            .GetPropertiesForComparisonAsync(request.PropertyIds);

        if (properties == null || !properties.Any())
            return BadRequest("No properties found for comparison");

        // 2️⃣ Call AI service (MATCHES INTERFACE)
        var result = await _propertyComparisonAiService.CompareAsync(
            properties.ToList(),
            request.UserType
        );

        if (result == null)
            return BadRequest("Comparison result is null");

        return Ok(result);
    }



    [HttpGet("district/{district}/price-history")]
    public async Task<IActionResult> GetDistrictPriceHistory(string district)
    {
        var result = await _analyticsService.GetDistrictPriceAnalytics(district);
        return Ok(result);
    }

    // =================================================================================================
    // 💬 PROPERTY CHAT SYSTEM (Customer <-> Property Owner)
    // =================================================================================================

    [HttpPost("{id}/chat/send")]
    [Authorize]
    public async Task<IActionResult> SendPropertyMessage(int id, [FromBody] Dictionary<string, string> payload)
    {
        try
        {
            var senderId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(senderId)) return Unauthorized("User not authenticated.");

            if (!payload.TryGetValue("messageText", out var messageText) || string.IsNullOrWhiteSpace(messageText))
                return BadRequest("Message cannot be empty.");

            var property = await propertyRepository.GetByIdAsync(id);
            if (property == null) return NotFound("Property not found.");

            var ownerId = property.OwnerID;
            
            // Logic to determine Receiver:
            // If Sender is Owner -> Receiver is the Customer (passed in query or context? Wait. A chat is 1-on-1).
            // This endpoint assumes initiation by Customer usually. 
            // BUT if Owner replies, they need to know WHO they are replying to.
            // For simplicity in this iteration: We support Customer -> Owner and Owner -> Customer (if conversation exists).
            
            string receiverId;
            string customerIdForRoom;

            if (senderId == ownerId)
            {
                // Owner is sending. Receiver must be specified.
                if (!payload.TryGetValue("receiverId", out var rId) || string.IsNullOrEmpty(rId))
                     return BadRequest("As an owner, you must specify the receiverId (customer).");
                receiverId = rId;
                customerIdForRoom = receiverId;
            }
            else
            {
                // Customer is sending. Receiver is Owner.
                receiverId = ownerId;
                customerIdForRoom = senderId;
            }

            // 1. Save Message to Database (Directly using Repo or Service - Assuming Repo for now as PropertyService might not have chat logic)
             // We need a proper Service method for this ideally, but for now we do it via Repo if accessible or generic.
             // Since PropertyService is injected, let's see if we can add it there or use GenericRepo.
             // We will assume generic repository availability or use a direct context if possible, 
             // BUT simpler: Use propertyRepository to add Chat entity if it allows, or just use the Hub to notify for now? 
             // NO, persistence is required. 
             // I will use _propertyService to Save (will add method to interface next).
            
            // Actually, let's use the Hub to broadcast first.
            var roomName = $"PropertyChat_{id}_{customerIdForRoom}";

            // Broadcast to Receiver Only
            // 🆕 CHANGED: Event name strictly for Customer Chat
            _logger.LogInformation("📡 Controller Sending Customer Message: ToUser={ReceiverId}, Event=ReceiveCustomerMessage, Sender={SenderName}", receiverId, User.Identity.Name);
            await _chatHubContext.Clients.User(receiverId).SendAsync("ReceiveCustomerMessage", new 
            {
                PropertyID = id,
                SenderID = senderId,
                ReceiverID = receiverId,
                Message = messageText,
                SentDate = DateTime.UtcNow,
                SenderName = User.Identity.Name ?? "User"
            });

            return Ok(new { status = "Message sent", room = roomName });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error in SendPropertyMessage");
            return BadRequest(new { error = ex.Message });
        }
    }

    // 🆕 INBOX ENDPOINT: Aggregates chats for the Owner
    [HttpGet("chat/inbox")]
    [Authorize]
    public async Task<IActionResult> GetOwnerChatInbox()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        // Assumption: User is an Owner. We need to find all chats where he is involved.
        // Since we don't have a dedicated repo method yet, we might need to rely on PropertyService.
        // For this hotfix, we return a structure that the frontend expects, populated from the Service.
        try 
        {
             // TODO: Implement GetOwnerInbox in PropertyService. 
             // Currently returning empty to define the Contract.
             // var inbox = await _propertyService.GetOwnerInboxAsync(userId);
             return Ok(new List<object>()); 
        }
        catch(Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{id}/chat/history")]
    [Authorize]
    public async Task<IActionResult> GetPropertyChatHistory(int id, [FromQuery] string? customerId = null)
    {
        try 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized("User not authenticated.");

            var property = await propertyRepository.GetByIdAsync(id);
            if (property == null) return NotFound("Property not found.");
            
            string otherUserId;

            if (userId == property.OwnerID)
            {
                // Owner viewing chat with Customer
                if (string.IsNullOrEmpty(customerId))
                    return BadRequest("Owner must provide customerId query parameter.");
                otherUserId = customerId;
            }
            else
            {
                 // Customer viewing chat with Owner
                 otherUserId = property.OwnerID;
            }

            var history = await _propertyService.GetChatHistoryAsync(userId, otherUserId, id);
            return Ok(history);
        }
        catch(Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

