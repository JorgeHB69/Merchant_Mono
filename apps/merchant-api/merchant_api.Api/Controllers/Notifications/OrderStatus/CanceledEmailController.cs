using MassTransit;
using merchant_api.Data.Models.Concretes.Emails.OrderStatusEmails;
using Microsoft.AspNetCore.Mvc;

namespace merchant_api.Api.Controllers.Notifications.OrderStatus
{
    [ApiController]
    [Route("api/notification/order/canceled")]
    [Tags("Notifications")]
    public class CanceledEmailController(IBus producer) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Send([FromBody] CanceledEmail canceledEmail)
        {
            await producer.Publish(canceledEmail);
            return Ok();
        }
    }
}