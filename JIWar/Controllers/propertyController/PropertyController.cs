using GEWAR.Models;
using Jiwar.DTOs;
using Jiwar.DTOs.PropertyDTOs;
using Jiwar.Models;
using Jiwar.Service;
using Jiwar.Services;
using JIWar.PropertyOwner;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    private readonly IPropertyAnalyticsService _analyticsService;


    public PropertyController(IPropertyService propertyService , IPropertyAnalyticsService analyticsService)
    {
        _propertyService = propertyService;
        _analyticsService = analyticsService;

    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(PropertyCreateDTO dto)
    {
        var property = new Property
        {
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            OwnerID = dto.OwnerId,
            CategoryId = dto.CategoryId,
            Tour360Url = dto.Tour360Url // حفظ الرابط الجديد
        };

        await _propertyService.AddPropertyAsync(property);

        // حساب السعر المقترح وحفظه في PropertyAnalytics
        var analytics = await _analyticsService.AnalyzePropertyAsync(property);

    string priceStatus = property.Price > analytics.FairValue_Estimate ? "Overpriced" :
                         property.Price < analytics.FairValue_Estimate ? "Underpriced" : "Fair";

    return Ok(new
    {
        propertyId = property.PropertyID,
        ownerPrice = property.Price,
        estimatedPrice = analytics.FairValue_Estimate,
        priceStatus = priceStatus,
        tour360Url = property.Tour360Url
    });

    }

    [HttpPut("update")]
    public async Task<IActionResult> Update(PropertyUpdateDTO dto)
    {
        var property = new Property
        {
            PropertyID = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price
        };

        var result = await _propertyService.UpdatePropertyAsync(property);
        return result ? Ok("Updated Successfully") : NotFound("Property Not Found");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _propertyService.DeletePropertyAsync(id);
        return result ? Ok("Deleted Successfully") : NotFound("Property Not Found");
    }

    [HttpGet("my/{ownerId:int}")]
    public async Task<IActionResult> MyProperties(int ownerId)
    {
        var list = await _propertyService.GetMyPropertiesAsync(ownerId);
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var property = await _propertyService.GetPropertyDetailsAsync(id); 
        return property != null ? Ok(property) : NotFound();
    }

    [HttpGet("{id}/share")]
    public async Task<IActionResult> Share(int id)
    {
        var property = await _propertyService.GetPropertyDetailsAsync(id);
        if (property == null || property.IsDeleted) return NotFound();

        var dto = new SharePreviewDTO
        {
            Url = $"https://yourdomain.com/properties/{id}",
            Title = property.Title,
            Description = property.Description,
            ImageUrl = property.PropertyMedia?.FirstOrDefault()?.MediaURL,
            Price = property.Price
        };

        return Ok(dto);
    }

    [HttpGet("browse")]
    public async Task<IActionResult> Browse([FromQuery] PropertyFilterDTO filter)
    {
        var properties = await _propertyService.GetFilteredPropertiesAsync(filter);
        return Ok(properties);
    }

    [HttpPost("compare")]
    public async Task<IActionResult> Compare([FromBody] List<int> propertyIds)
    {
        if (propertyIds == null || propertyIds.Count == 0 || propertyIds.Count > 5)
            return BadRequest("You must provide between 1 and 5 property IDs.");

        var result = await _propertyService.GetPropertiesForComparisonAsync(propertyIds);
        return Ok(result);
    }

}
