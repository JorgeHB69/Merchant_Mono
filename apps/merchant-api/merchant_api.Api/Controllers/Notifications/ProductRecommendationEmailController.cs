using MassTransit;
using merchant_api.Data.Models.Concretes.Emails;
using Microsoft.AspNetCore.Mvc;

namespace merchant_api.Api.Controllers.Notifications
{
    [ApiController]
    [Route("api/notification/recommendations")]
    [Tags("Notifications")]
    public class ProductRecommendationEmailController(IBus producer) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Send([FromBody] ProductRecommendationEmail productRecommendationEmail)
        {
            await producer.Publish(productRecommendationEmail);
            return Ok();
        }
    }
}