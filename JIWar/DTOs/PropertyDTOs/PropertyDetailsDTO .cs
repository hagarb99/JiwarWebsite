namespace JIWar.PropertyOwner
{
    public class PropertyDetailsDTO : PropertyCreateDTO
{
    public int Id { get; set; }
    public DateTime PublishedAt { get; set; }
    public List<string> MediaUrls { get; set; }
}
}


