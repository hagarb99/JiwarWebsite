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

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
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
        [HttpPost("pay")]
        public async Task<IActionResult> PayForBooking([FromBody] BuyBookingDto dto)
        {
            var paymentUrl = await _paymentService.CreateBookingPaymentRequest(dto.UserId, dto.BookingId, dto.Method);
            return Ok(new { paymentUrl });
        }

        [HttpPost("payment/webhook")]
        public async Task<IActionResult> BookingPaymentWebhook([FromBody] PaymentWebhookDto dto)
        {
            var success = await _paymentService.ConfirmBookingPayment(dto.Reference);
            if (!success)
                return BadRequest("Invalid reference");

            return Ok("Booking payment confirmed");
        }

        [Authorize]
        [HttpGet("confirm/{id}")]
        public async Task<IActionResult> ConfirmBooking(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var hasPaid = await _paymentService.HasUserPaidForBooking(userId, id);

            if (!hasPaid)
                return Forbid("You must complete payment to confirm this booking.");

            var booking = await _service.GetByIdAsync(id);
            return Ok(booking);
        }
    }

}
