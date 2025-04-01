using MassTransit;
using merchant_api.Data.Models.Concretes.Emails.OrderStatusEmails;
using Microsoft.AspNetCore.Mvc;

namespace merchant_api.Api.Controllers.Notifications.OrderStatus
{
    [ApiController]
    [Route("api/notification/order/pending")]
    [Tags("Notifications")]
    public class PendingEmailController(IBus producer) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Send([FromBody] PendingEmail pendingEmail)
        {
            await producer.Publish(pendingEmail);
            return Ok();
        }
    }
}