public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepo;

    public PropertyService(IPropertyRepository propertyRepo)
    {
        _propertyRepo = propertyRepo;
    }

    // 1. Add Property
    public async Task<Property> AddPropertyAsync(Property property)
    {
        await _propertyRepo.AddAsync(property);
        return property;
    }

    // 2. Update Property
    public async Task<bool> UpdatePropertyAsync(Property property)
    {
        var existing = await _propertyRepo.GetByIdAsync(property.Id);
        if (existing == null) return false;

        _propertyRepo.Update(property);
        return true;
    }

    // 3. Delete Property (Soft Delete)
    public async Task<bool> DeletePropertyAsync(int id)
    {
        var property = await _propertyRepo.GetByIdAsync(id);
        if (property == null) return false;

        property.IsDeleted = true;
        _propertyRepo.Update(property);

        return true;
    }

    // 4. My Properties
    public Task<IEnumerable<Property>> GetMyPropertiesAsync(string ownerId)
    {
        return _propertyRepo.GetMyPropertiesAsync(ownerId);
    }

    // 5. Property Details
    public Task<Property> GetPropertyDetailsAsync(int id)
    {
        return _propertyRepo.GetPropertyDetailsAsync(id);
    }
}
