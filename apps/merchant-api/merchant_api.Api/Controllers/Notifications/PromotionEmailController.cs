using MassTransit;
using merchant_api.Data.Models.Concretes.Emails;
using Microsoft.AspNetCore.Mvc;

namespace merchant_api.Api.Controllers.Notifications
{
    [ApiController]
    [Route("api/notification/promotions")]
    [Tags("Notifications")]
    public class PromotionEmailController(IBus producer) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Send([FromBody] PromotionEmail promotionEmail)
        {
            await producer.Publish(promotionEmail);
            return Ok();
        }
    }
}