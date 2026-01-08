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

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    private readonly IPropertyAnalyticsService _analyticsService;
    private readonly IPropertyRepository propertyRepository;
    private readonly IPropertyComparisonAiService _propertyComparisonAiService;

        public PropertyController(IPropertyService propertyService,
        IPropertyAnalyticsService analyticsService,
        IPropertyComparisonAiService propertyComparisonAiService,
        IPropertyRepository propertyRepository
        )
    {
        _propertyService = propertyService;
        _analyticsService = analyticsService;
        _propertyComparisonAiService = propertyComparisonAiService;
        this.propertyRepository = propertyRepository;
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



    [HttpPost("compare")]
    [AllowAnonymous]
    public async Task<IActionResult> Compare([FromBody] PropertyComparisonRequestDTO request)
    {
        if (request.PropertyIds.Count < 2 || request.PropertyIds.Count > 5)
            return BadRequest("Choose between 2 and 5 properties.");

        var comparisonDtos = (await _propertyService.GetPropertiesForComparisonAsync(request.PropertyIds))
                             .ToList();

        var aiResult = await _propertyComparisonAiService.CompareAsync(comparisonDtos, request.UserType);

        return Ok(aiResult);
    }


    [HttpGet("district/{district}/price-history")]
    public async Task<IActionResult> GetDistrictPriceHistory(string district)
    {
        var result = await _analyticsService.GetDistrictPriceAnalytics(district);
        return Ok(result);
    }
}

