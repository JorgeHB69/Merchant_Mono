using merchant_api.Business.Services.Email.Bases;
using merchant_api.Business.Services.Email.Concretes;
using merchant_api.Business.Services.Email.Templates.OrdersStatus;
using merchant_api.Data.Models.Concretes.Emails.OrderStatusEmails;
using merchant_api.Data.Models.Interfaces.OrderStatusEmails;
using Microsoft.AspNetCore.Mvc;

namespace merchant_api.Api.Controllers.Notifications.OrderStatus
{
    [ApiController]
    [Route("api/notification/order/delivered")]
    [Tags("Notifications")]
    public class DeliveredEmailController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Send([FromBody] DeliveredEmail deliveredEmail)
        {
            EmailTemplateService<IOrderStatusWithTimeEmail> service = new StatusWithTImeTemplateService("order-delivered");
            var emailService = new EmailService<IOrderStatusWithTimeEmail>(service);
            await emailService.Send(deliveredEmail.Contact.ContactEmail, "Order status", deliveredEmail);
            return Ok();
        }
    }
}