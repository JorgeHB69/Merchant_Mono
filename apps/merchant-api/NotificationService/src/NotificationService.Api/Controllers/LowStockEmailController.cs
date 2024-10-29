using Microsoft.AspNetCore.Mvc;
using NotificationService.Domain.Dtos.Emails;

namespace NotificationService.Api.Controllers
{
    [ApiController]
    [Route("api/low-stock")]
    public class LowStockEmailController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Send([FromBody] LowStockEmail lowStockEmail)
        {
            return Ok();
        }
    }
}