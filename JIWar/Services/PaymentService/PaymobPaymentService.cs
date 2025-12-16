using GEWAR.Models;
using Jiwar.DTOs;
using Jiwar.Models;
using Jiwar.Repositories;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Jiwar.Services
{
    public class PaymobPaymentService : IPaymentService
    {
        private readonly IBookingPaymentRepository _bookingPaymentRepo;
        private readonly IReportOrderRepository _reportOrderRepo;
        private readonly IConfiguration _config;
        private readonly HttpClient _http;

        private readonly string _apiKey;
        private readonly string _integrationId;
        private readonly string _iframeBaseUrl;
        private readonly string _hmacSecret;

        public PaymobPaymentService(IBookingPaymentRepository bookingPaymentRepo,
            IReportOrderRepository reportOrderRepo,
            IConfiguration config)
        {
            _bookingPaymentRepo = bookingPaymentRepo;
            _reportOrderRepo = reportOrderRepo;
            _config = config;

            _apiKey = _config["Paymob:ApiKey"]!;
            _integrationId = _config["Paymob:IntegrationId"]!;
            _iframeBaseUrl = _config["Paymob:IframeUrl"]!;
            _hmacSecret = _config["Paymob:HmacSecret"]!;

            _http = new HttpClient();
        }

        // Booking Payment
        public async Task<string> CreateBookingPaymentAsync(string userId, int bookingId)
        {
            // Fetch booking and amount
            var bookingPayment = await _bookingPaymentRepo.GetByBookingIdAsync(bookingId);
            if (bookingPayment == null)
                throw new Exception("Booking not found");

            int amountCents = (int)(bookingPayment.Amount * 100);

            //Get auth token
            string authToken = await GetAuthToken();

            //Create order in Paymob
            long orderId = await CreateOrder(authToken, amountCents, bookingId.ToString());

            //Create payment key
            string paymentKey = await CreatePaymentKey(authToken, amountCents, orderId, userId);

            //Return iframe URL
            string iframeUrl = $"{_iframeBaseUrl}?payment_token={paymentKey}";

            return iframeUrl;
        }
        
        //Report Payment
        public async Task<string> CreateReportPaymentAsync(string userId, int reportId)
        {
            var order = await _reportOrderRepo.GetByReportIdAsync(reportId);
            if (order == null)
                throw new Exception("Report not found");

            int amountCents = (int)(order.Amount * 100);

            string authToken = await GetAuthToken();

            long orderId = await CreateOrder(authToken, amountCents, reportId.ToString());

            string paymentKey = await CreatePaymentKey(authToken, amountCents, orderId, userId);

            string iframeUrl = $"{_iframeBaseUrl}?payment_token={paymentKey}";

            return iframeUrl;
        }

        //Handle webhook
        public async Task HandlePaymobWebhookAsync(PaymobWebhookDto dto)
        {
            // Validate HMAC
            if (!ValidateHmac(dto))
                throw new Exception("Invalid HMAC");

            if (!dto.Success || dto.Pending || dto.IsRefunded)
                return; // ignore failed or pending payments

            // Booking payment
            var bookingPayment = await _bookingPaymentRepo.GetByOrderIdAsync(dto.Obj.OrderId);
            if (bookingPayment != null)
            {
                bookingPayment.PaymentStatus = PaymentStatusEnum.Completed;
                await _bookingPaymentRepo.UpdateAsync(bookingPayment);
                return;
            }

            // Report payment
            var reportOrder = await _reportOrderRepo.GetByOrderIdAsync(dto.Obj.OrderId);
            if (reportOrder != null)
            {
                reportOrder.PaymentStatus = PaymentStatusEnum.Completed;
                await _reportOrderRepo.UpdateAsync(reportOrder);
            }
        }

        
        //Check payment status
        public async Task<bool> HasUserPaidForBookingAsync(string userId, int bookingId)
        {
            return await _bookingPaymentRepo.HasUserPaidForBooking(userId, bookingId);
        }

        public async Task<bool> HasUserPaidForReportAsync(string userId, int reportId)
        {
            return await _reportOrderRepo.HasUserPaidForReport(userId, reportId);
        }

        // Helper: Get Auth Token
        private async Task<string> GetAuthToken()
        {
            var payload = new { api_key = _apiKey };
            var response = await _http.PostAsJsonAsync("https://accept.paymob.com/api/auth/tokens", payload);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(json)!;
            return data.token;
        }

        // Helper: Create Order
        private async Task<long> CreateOrder(string authToken, int amountCents, string merchantOrderId)
        {
            var payload = new
            {
                auth_token = authToken,
                delivery_needed = false,
                amount_cents = amountCents,
                currency = "EGP",
                merchant_order_id = merchantOrderId
            };

            var response = await _http.PostAsJsonAsync("https://accept.paymob.com/api/ecommerce/orders", payload);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(json)!;
            return data.id;
        }

        // Helper: Create Payment Key
        private async Task<string> CreatePaymentKey(string authToken, int amountCents, long orderId, string userId)
        {
            var billingData = new
            {
                apartment = "",
                email = "user@example.com",
                floor = "",
                first_name = userId,
                street = "",
                building = "",
                phone_number = "0000000000",
                shipping_method = "NA",
                postal_code = "NA",
                city = "NA",
                country = "EG",
                last_name = "NA"
            };

            var payload = new
            {
                auth_token = authToken,
                amount_cents = amountCents,
                expiration = 3600,
                order_id = orderId,
                billing_data = billingData,
                currency = "EGP",
                integration_id = _integrationId
            };

            var response = await _http.PostAsJsonAsync("https://accept.paymob.com/api/acceptance/payment_keys", payload);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(json)!;
            return data.token;
        }

        // Helper: Validate HMAC
        private bool ValidateHmac(PaymobWebhookDto dto)
        {
            var sortedKeys = new SortedDictionary<string, string>
            {
                { "amount_cents", dto.Obj.AmountCents.ToString() },
                { "created_at", DateTime.UtcNow.ToString() }, 
                { "currency", dto.Obj.Currency },
                { "error_occured", "false" }, 
                { "has_parent_transaction", "false" },
                { "id", dto.Obj.Id.ToString() },
                { "integration_id", _integrationId },
                { "is_3d_secure", "false" },
                { "is_auth", "false" },
                { "is_capture", "true" },
                { "is_refunded", dto.IsRefunded.ToString().ToLower() },
                { "is_standalone_payment", "true" },
                { "is_voided", "false" },
                { "merchant_order_id", dto.Obj.MerchantOrderId },
                { "order", dto.Obj.OrderId.ToString() },
                { "pending", dto.Pending.ToString().ToLower() },
                { "success", dto.Success.ToString().ToLower() }
            };

            var payload = string.Join("", sortedKeys.Select(k => k.Value));

            using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(_hmacSecret));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            var computedHmac = BitConverter.ToString(hash).Replace("-", "").ToLower();

            return computedHmac == dto.Hmac.ToLower();
        }
    }
}
