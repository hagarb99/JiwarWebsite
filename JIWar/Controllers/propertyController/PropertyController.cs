using GEWAR;
using GEWAR.Models;
using Jiwar.DTOs;
using Jiwar.DTOs.PropertyDTOs;
using Jiwar.Models;
using Jiwar.Service;
using Jiwar.Services;
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
    private readonly GiwarContext _context;

    public PropertyController(IPropertyService propertyService
        , IPropertyAnalyticsService analyticsService
        , GiwarContext context,
        IPropertyRepository propertyRepository
        )
    {
        _propertyService = propertyService;
        _analyticsService = analyticsService;
        _context = context;
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

    [HttpPut("update")]
    public async Task<IActionResult> Update(PropertyUpdateDTO dto)
    {
        var ownerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (ownerId == null) return Unauthorized();

        // اختياري: تحقق إن العقار ده بتاع اليوزر ده (للأمان)
        var existing = await propertyRepository.GetByIdAsync(dto.Id);
        if (existing == null || existing.OwnerID != ownerId)
            return Forbid(); // أو NotFound

        var result = await _propertyService.UpdatePropertyAsync(dto);
        return result ? Ok("Updated Successfully") : NotFound("Property Not Found");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _propertyService.DeletePropertyAsync(id);
        return result ? Ok("Deleted Successfully") : NotFound("Property Not Found");
    }

    [HttpGet("my/{ownerId}")]
    public async Task<IActionResult> MyProperties(string ownerId)
    {
        var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (currentUserId != ownerId)
            return Forbid(); // عشان محدش يشوف عقارات غيرك

        var list = await _propertyService.GetMyPropertiesAsync(ownerId);
        return Ok(list);
        //var user = User.Claims;
        //var list = await _propertyService.GetMyPropertiesAsync(ownerId);
        //return Ok(list);
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

    [HttpGet("browse")]
    [AllowAnonymous]
    public async Task<IActionResult> Browse([FromQuery] PropertyFilterDTO filter)
    {
        var properties = await _propertyService.GetFilteredPropertiesAsync(filter);
        return Ok(properties);
    }

    [HttpPost("compare")]
    [AllowAnonymous]
    public async Task<IActionResult> Compare([FromBody] List<int> propertyIds)
    {
        if (propertyIds == null || propertyIds.Count == 0 || propertyIds.Count > 5)
            return BadRequest("You must provide between 1 and 5 property IDs.");

        var result = await _propertyService.GetPropertiesForComparisonAsync(propertyIds);
        return Ok(result);
    }

    [HttpGet("district/{district}/price-history")]
    public async Task<IActionResult> GetDistrictPriceHistory(string district)
    {
        var result = await _analyticsService.GetDistrictPriceAnalytics(district);
        return Ok(result);
    }
}

