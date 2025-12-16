namespace Jiwar.DTOs.DesignDto
{
    public class DesignDto
    {
        public int Id { get; set; }
        public string DesignerID { get; set; }
        public string DesignURL { get; set; }
        public bool AI_Generated { get; set; }
        public string SelectedStyle { get; set; }
        public DateTime CreationDate { get; set; }
    }

}
