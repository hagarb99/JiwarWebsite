using GEWAR.Models;



namespace Jiwar.Models
{
    public class DesignRequest : BaseModel
    {
        public string UserID { get; set; }           // صاحب الطلب (الـ Owner)
        public int PropertyID { get; set; }          // العقار اللي عايز تصميم
        public string PreferredStyle { get; set; }   // الستايل المطلوب
        public decimal? Budget { get; set; }         // الميزانية
        public string Notes { get; set; }            // ملاحظات إضافية

        public List<string> ImageURLs { get; set; } = new(); // نضمن إنها مش null

        public bool IsForSaleEnhancement { get; set; } // لتحسين البيع ولا لأ
        public string Status { get; set; }             // Open, HasProposals, InProgress, Completed
        public DateTime CreatedAt { get; set; }

        public virtual List<DesignerProposal> Proposals { get; set; } = new(); // تربط الطلب بالعروض
    }
}
