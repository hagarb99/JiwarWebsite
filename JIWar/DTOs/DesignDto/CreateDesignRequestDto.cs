

namespace Jiwar.DTOs.DesignDto
{
    public class CreateDesignDto
    {

        public int PropertyID { get; set; }
        public int ProposalID { get; set; }
        public List<string> ImageURLs { get; set; }
        public bool AI_Generated { get; set; }
        public string SelectedStyle { get; set; }
        public string Description { get; set; }
    }

}

