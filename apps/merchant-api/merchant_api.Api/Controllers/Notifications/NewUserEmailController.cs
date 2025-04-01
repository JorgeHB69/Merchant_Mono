using MassTransit;
using merchant_api.Data.Models.Concretes.Emails;
using Microsoft.AspNetCore.Mvc;

namespace merchant_api.Api.Controllers.Notifications
{
    [ApiController]
    [Route("api/notification/new-user")]
    [Tags("Notifications")]
    public class NewUserEmailController(IBus producer) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Send([FromBody] NewUserEmail newUserEmail)
        {
            await producer.Publish(newUserEmail);
            return Ok();
        }
    }
}