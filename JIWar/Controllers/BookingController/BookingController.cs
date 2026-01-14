using Jiwar.DTOs;
using Jiwar.DTOs.BookingDTOs;
using Jiwar.Repositories;
using Jiwar.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace Jiwar.Controllers
{


    [Route("api/[controller]")] 
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _service;
        private readonly IPaymentService _paymentService;

        public BookingController(IBookingService service, IPaymentService paymentService)
        {
            _service = service;
            _paymentService = paymentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }
        //[Authorize]
        //[HttpGet("PropertyOwner")]
        //public async Task<IActionResult> GetOwnerBookings()
        //{
        //    var ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var bookings = await _service.GetBookingsForOwnerAsync(ownerId);
        //    return Ok(bookings);
        //}
        //[Authorize]
        //[HttpPut("{id}/status")]
        //public async Task<IActionResult> UpdateBookingStatus(int id, [FromBody] UpdateBookingStatusDto dto)
        //{
        //    var ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var success = await _service.UpdateBookingStatusAsync(id, dto.Status, ownerId);

        //    if (!success)
        //        return Forbid();

        //    return NoContent();
        //}


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingDto dto)
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var result = await _service.CreateAsync(dto, customerId);
                return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
            }
            catch (Exception ex) when (ex.Message == "FREE_LIMIT_REACHED")
            {
                // 403 Forbidden تعني أن الوصول ممنوع حالياً إلا بشرط (الاشتراك)
                return StatusCode(403, new
                {
                    message = "لقد استنفدت الحجز المجاني الواحد. يرجى الاشتراك للمتابعة.",
                    redirectTo = "/subscriptions"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }

        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateBookingDto dto)
        {
            var done = await _service.UpdateAsync(id, dto);
            if (!done) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var done = await _service.DeleteAsync(id);
            if (!done) return NotFound();

            return NoContent();
        }

        //payment for booking
        [Authorize]
        [HttpPost("pay")]
        public async Task<IActionResult> PayForBooking([FromBody] BuyBookingDto dto)
        {
            var paymentUrl = await _paymentService.CreateBookingPaymentAsync(dto.UserId, dto.BookingId);
            return Ok(new { paymentUrl });
        }

        [HttpPost("payment/webhook")]
        public async Task<IActionResult> BookingPaymentWebhook([FromBody] PaymobWebhookDto dto)
        {
            await _paymentService.HandlePaymobWebhookAsync(dto);
            return Ok("Booking payment confirmed");
        }

        [Authorize]
        [HttpGet("confirm/{id}")]
        public async Task<IActionResult> ConfirmBooking(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var hasPaid = await _paymentService.HasUserPaidForBookingAsync(userId, id);

            if (!hasPaid)
                return Forbid("You must complete payment to confirm this booking.");

            var booking = await _service.GetByIdAsync(id);
            return Ok(booking);
        }
        [Authorize]
        [HttpGet("customer")]
        public async Task<IActionResult> GetCustomerBookings()
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _service.GetBookingsByCustomerAsync(customerId));
        }

        [Authorize]
        [HttpGet("owner")]
        public async Task<IActionResult> GetOwnerBookings()
        {
            var ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _service.GetBookingsForOwnerAsync(ownerId));
        }

        [Authorize]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateBookingStatus(int id, UpdateBookingStatusDto dto)
        {
            if (dto == null)
                return BadRequest("Request body is missing");

            var ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var success = await _service.UpdateBookingStatusAsync(id, dto.Status, ownerId);

            if (!success) return Forbid();
            return NoContent();
        }



    }

}
 