namespace Jiwar.DTOs
{
   public class UpdateSimulationDetailsDto
{
    public decimal Size { get; set; }
    public int Rooms { get; set; }
    public int Bathrooms { get; set; }
    public string Condition { get; set; } = string.Empty;
}    
}

