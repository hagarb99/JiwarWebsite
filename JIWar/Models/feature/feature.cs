namespace GEWAR.Models
{
    public class Feature
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation
    public virtual ICollection<PropertyFeature> PropertyFeatures { get; set; }
}

}
