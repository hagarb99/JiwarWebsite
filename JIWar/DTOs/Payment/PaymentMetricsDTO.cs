namespace Jiwar.DTOs.Payment
{
    
        public class PaymentMetricsDTO
        {
            public decimal TotalRevenue { get; set; }



            // 📅 Today / Week / Month
            public Dictionary<string, decimal> RevenueByPeriod { get; set; }

            // 💳 Booking / Subscription / Report
            public Dictionary<string, decimal> RevenueByType { get; set; }

            public int TotalTransactions { get; set; }


            public PaymentMetricsDTO()
            {
                RevenueByPeriod = new Dictionary<string, decimal>();
                RevenueByType = new Dictionary<string, decimal>();
            }
        }



    }

