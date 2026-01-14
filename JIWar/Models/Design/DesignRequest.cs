using GEWAR.Models;



namespace Jiwar.Models
{
    public class DesignRequest : BaseModel
    {
        public string UserID { get; set; } = null!;           // صاحب الطلب (الـ Owner)
        public virtual User User { get; set; } = null!;

        public int PropertyID { get; set; }          // العقار اللي عايز تصميم

        public virtual Property Property { get; set; } = null!;

        public string PreferredStyle { get; set; } = string.Empty;   // الستايل المطلوب
        public decimal? Budget { get; set; }         // الميزانية
        public string Notes { get; set; } = string.Empty;            // ملاحظات إضافية
        public StatusEnumReqPro StatusEnumRequest { get; set; }

        public List<string> ImageURLs { get; set; } = new(); // نضمن إنها مش null

        public bool IsForSaleEnhancement { get; set; } // لتحسين البيع ولا لأ
        public string Status { get; set; } = "Open";             // Open, HasProposals, InProgress, Completed
        public DateTime CreatedAt { get; set; }
        public virtual RequestRating? RequestRating { get; set; } 
      
        public virtual ICollection<Design> Designs { get; set; } = new List<Design>();

        public virtual ICollection<DesignerProposal> Proposals { get; set; } = new List<DesignerProposal>();// تربط الطلب بالعروض
    }
}
