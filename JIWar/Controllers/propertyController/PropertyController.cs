using GEWAR.Models;
using Jiwar.DTOs;
using Jiwar.DTOs.PropertyDTOs;
using Jiwar.Models;
using Jiwar.Service;
using JIWar.PropertyOwner;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
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
            CategoryId = dto.CategoryId
        };

        await _propertyService.AddPropertyAsync(property);
        return Ok("Property Created Successfully");
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

}




