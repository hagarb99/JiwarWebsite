namespace Jiwar.DTOs.AdminAnalytics
{
    
        public class AdminWishlistDto
        {
            public int Id { get; set; }

        public string UserId { get; set; }        // ✅
        public int PropertyId { get; set; }
        public string UserName { get; set; }
            public string PropertyTitle { get; set; }
            public DateTime AddedDate { get; set; }
            public string Notes { get; set; }
        }
    }



