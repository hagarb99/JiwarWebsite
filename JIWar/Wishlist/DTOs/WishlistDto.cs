namespace Jiwar.DTOs
{
    public class WishlistDto
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public int PropertyID { get; set; }
        public DateTime AddedDate { get; set; }
        public string Notes { get; set; }
    }

}
