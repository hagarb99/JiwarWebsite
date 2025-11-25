using GEWAR.Models;
using Microsoft.AspNetCore.Mvc;
using JIWar.PropertyOwner;
using Jiwar.Models;
using Jiwar.Service;

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
            OwnerId = dto.OwnerId,
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
            Id = dto.Id,
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

    [HttpGet("my/{ownerId}")]
    public async Task<IActionResult> MyProperties(string ownerId)
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
}




