using GEWAR.Models;

namespace Jiwar.Models
{
    public class SimulationDetails : BaseModel
    {
        public int RenovationSimulationID { get; set; }

        public decimal Size { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public string Condition { get; set; }
        public int? YearBuilt { get; set; }

        public virtual RenovationSimulation RenovationSimulation { get; set; }
    }
}