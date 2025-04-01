using merchant_api.Business.Services.Email.Bases;
using merchant_api.Business.Services.Email.Concretes;
using merchant_api.Business.Services.Email.Templates.OrdersStatus;
using merchant_api.Data.Models.Concretes.Emails.OrderStatusEmails;
using merchant_api.Data.Models.Interfaces.OrderStatusEmails;
using Microsoft.AspNetCore.Mvc;

namespace merchant_api.Api.Controllers.Notifications.OrderStatus
{
    [ApiController]
    [Route("api/notification/order/returned")]
    [Tags("Notifications")]
    public class ReturnedEmailController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Send([FromBody] ReturnedEmail returnedEmail)
        {
            EmailTemplateService<IOrderStatusWithTimeEmail> service = new StatusWithTImeTemplateService("order-returned");
            var emailService = new EmailService<IOrderStatusWithTimeEmail>(service);
            await emailService.Send(returnedEmail.Contact.ContactEmail, "Order status", returnedEmail);
            return Ok();
        }
    }
}