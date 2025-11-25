using Microsoft.AspNetCore.Mvc;
using JIWar.PropertyOwner;
using Jiwar.Models;
// using Jiwar.Services.interfaces;

namespace Jiwar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        //commented to try to resolve IPropertyService
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _subscriptionService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var subscription = await _subscriptionService.GetByIdAsync(id);
            return subscription != null ? Ok(subscription) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubscriptionCreateDTO dto)
        {
            var result = await _subscriptionService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(SubscriptionUpdateDTO dto)
        {
            var success = await _subscriptionService.UpdateAsync(dto);
            return success ? Ok("Updated Successfully") : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _subscriptionService.DeleteAsync(id);
            return success ? Ok("Deleted Successfully") : NotFound();
        }
    }
}
