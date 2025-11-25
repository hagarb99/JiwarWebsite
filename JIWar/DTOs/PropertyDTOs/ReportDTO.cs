namespace Jiwar.DTOs.PropertyDTOs
{
    public class ReportDTO
    {
        public int Id { get; set; }
        public string ReportType { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DownloadUrl { get; set; }
    }
}
