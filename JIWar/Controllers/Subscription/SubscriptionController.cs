using GEWAR.Models;
using Jiwar.Models;
using Jiwar.Services;
using JIWar.PropertyOwner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
// using Jiwar.Services.interfaces;

namespace Jiwar.Controllers
{
   
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
 

    public class SubscriptionController : ControllerBase
    {
        //commented to try to resolve IPropertyService
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet("plans")]   
        public IActionResult GetPlans()
        {
            var plans = new List<Subscription>
    {
        new Subscription
        {
            Name = "Basic",
            Price = 10,
            DurationInMonths = 1,
            planTypeEnum = PlanTypeEnum.Basic
        },
        new Subscription
        {
            Name = "Golden",
            Price = 25,
            DurationInMonths = 3,
            planTypeEnum = PlanTypeEnum.Golden
        },
        new Subscription
        {
            Name = "Premium",
            Price = 50,
            DurationInMonths = 6,
            planTypeEnum = PlanTypeEnum.Premium
        }
    };

            return Ok(plans);
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var result = await _subscriptionService.CreateAsync(dto, userId);
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
