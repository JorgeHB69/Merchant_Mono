using MassTransit;
using merchant_api.Data.Models.Concretes.Emails.OrderStatusEmails;
using Microsoft.AspNetCore.Mvc;

namespace merchant_api.Api.Controllers.Notifications.OrderStatus
{
    [ApiController]
    [Route("api/notification/order/confirmed")]
    [Tags("Notifications")]
    public class ConfirmedEmailController(IBus producer) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Send([FromBody] ConfirmedEmail confirmedEmail)
        {
            await producer.Publish(confirmedEmail);
            return Ok();
        }
    }
}