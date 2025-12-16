namespace Jiwar.DTOs.RequestDto
{
    public class RequestDto
    {
        public int Id { get; set; }
        public string CustomerID { get; set; }
        public int PropertyID { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
