namespace GEWAR.Models
{
    public class SimulationMedia : BaseModel
{
    public int RenovationSimulationID { get; set; }

    public SimulationMediaTypeEnum MediaType { get; set; }
    public string FileUrl { get; set; }

    public virtual RenovationSimulation RenovationSimulation { get; set; }
}

}
