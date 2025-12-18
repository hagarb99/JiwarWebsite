namespace Jiwar.DTOs.DesignDto
{
    public class DesignRequestDto
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public int PropertyID { get; set; }
        public string PreferredStyle { get; set; }
        public decimal? Budget { get; set; }
        public string Notes { get; set; }
        public List<string> ImageURLs { get; set; }
        public bool IsForSaleEnhancement { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public int ProposalCount { get; set; } // مفيد في Dashboard: كام عرض جاله
    }
}
