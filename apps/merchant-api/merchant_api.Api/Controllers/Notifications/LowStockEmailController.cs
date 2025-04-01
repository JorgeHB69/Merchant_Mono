using MassTransit;
using merchant_api.Data.Models.Concretes.Emails;
using Microsoft.AspNetCore.Mvc;

namespace merchant_api.Api.Controllers.Notifications
{
    [ApiController]
    [Route("api/notification/low-stock")]
    [Tags("Notifications")]
    public class LowStockEmailController(IBus producer) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Send([FromBody] LowStockEmail lowStockEmail)
        {
            await producer.Publish(lowStockEmail);
            return Ok();
        }
    }
}