namespace Jiwar.DTOs.ValuationDTOs
{
    public class UserValuationListDTO
    {
        public int Id { get; set; }
        public string City { get; set; }
        public decimal Area { get; set; }
        public decimal MostLikelyPrice { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
