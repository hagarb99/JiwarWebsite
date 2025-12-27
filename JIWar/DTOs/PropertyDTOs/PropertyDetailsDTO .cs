namespace JIWar.PropertyOwner
{
    public class PropertyDetailsDTO
{
        public int PropertyID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public decimal? Area_sqm { get; set; }
        public int? NumBedrooms { get; set; }
        public int? NumBathrooms { get; set; }
        public string Tour360Url { get; set; }
        public decimal LocationLat { get; set; }
        public decimal LocationLang { get; set; }

        public string OwnerName { get; set; }
        public List<string> MediaUrls { get; set; } = new List<string>();
        public DateTime? PublishedAt { get; set; }

    }
}


