using merchant_api.Business.Services.Email.Bases;
using merchant_api.Business.Services.Email.Concretes;
using merchant_api.Business.Services.Email.Templates.OrdersStatus;
using merchant_api.Data.Models.Concretes.Emails.OrderStatusEmails;
using merchant_api.Data.Models.Interfaces.OrderStatusEmails;
using Microsoft.AspNetCore.Mvc;

namespace merchant_api.Api.Controllers.Notifications.OrderStatus
{
    [ApiController]
    [Route("api/notification/order/shipped")]
    [Tags("Notifications")]
    public class ShippedEmailController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Send([FromBody] ShippedEmail shippedEmail)
        {
            EmailTemplateService<IOrderStatusWithTimeEmail> service = new StatusWithTImeTemplateService("order-shipped");
            var emailService = new EmailService<IOrderStatusWithTimeEmail>(service);
            await emailService.Send(shippedEmail.Contact.ContactEmail, "Order status", shippedEmail);
            return Ok();
        }
    }
}