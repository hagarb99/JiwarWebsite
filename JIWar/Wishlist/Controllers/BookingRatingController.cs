using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jiwar.Controllers
{
    using System.Threading.Tasks;
    using GEWAR.Models;
    using global::Jiwar.Repositories.Jiwar.Services;
    using JIWAR.Models;
    using Microsoft.AspNetCore.Mvc;

    namespace Jiwar.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class BookingRatingController : ControllerBase
        {
            private readonly BookingRatingService _service;

            public BookingRatingController(BookingRatingService service)
            {
                _service = service;
            }

            // GET: api/BookingRating
            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var list = await _service.GetAllBookingRatingsAsync();
                return Ok(list);
            }

            // POST: api/BookingRating
            [HttpPost]
            public async Task<IActionResult> Add([FromBody] BookingRating br)
            {
                var added = await _service.AddBookingRatingAsync(br);
                return Ok(added);
            }

            // PUT: api/BookingRating/{id}
            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, [FromBody] BookingRating br)
            {
                br.Id = id;
                var updated = await _service.UpdateBookingRatingAsync(br);
                return Ok(updated);
            }

            // DELETE: api/BookingRating/{id}
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var result = await _service.DeleteBookingRatingAsync(id);
                if (!result)
                    return NotFound();
                return Ok();
            }
        }
    }

}
