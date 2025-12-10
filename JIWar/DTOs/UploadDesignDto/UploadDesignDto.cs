namespace Jiwar.DTOs.UploadDesignDto
{
    public class UploadDesignDto
    {
        public string DesignerID { get; set; }
        public int RequestID { get; set; }
        public string DesignURL { get; set; }
        public bool AI_Generated { get; set; }
        public string SelectedStyle { get; set; }
    }

}
