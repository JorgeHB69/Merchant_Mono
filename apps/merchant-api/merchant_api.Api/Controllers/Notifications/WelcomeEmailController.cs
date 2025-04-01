using MassTransit;
using merchant_api.Data.Models.Concretes.Emails;
using Microsoft.AspNetCore.Mvc;

namespace merchant_api.Api.Controllers.Notifications
{
    [ApiController]
    [Route("api/notification/[controller]")]
    [Tags("Notifications")]
    public class WelcomeEmailController(IBus producer) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Send([FromBody] WelcomeEmail welcomeEmail)
        {
            await producer.Publish(welcomeEmail);
            return Ok();
        }
    }
}