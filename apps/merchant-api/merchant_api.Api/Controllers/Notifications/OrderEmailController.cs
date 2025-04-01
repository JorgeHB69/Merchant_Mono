using MassTransit;
using merchant_api.Business.Services.Email.Bases;
using merchant_api.Business.Services.Email.Concretes;
using merchant_api.Business.Services.Email.Templates;
using merchant_api.Data.Models.Concretes.Emails;
using Microsoft.AspNetCore.Mvc;

namespace merchant_api.Api.Controllers.Notifications
{
    [ApiController]
    [Route("api/notification/order")]
    [Tags("Notifications")]
    public class OrderEmailController(IBus producer) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Send([FromBody] OrderEmail order)
        {
            await producer.Publish(order);
            return Ok();
        }

        [HttpPost("discount")]
        public async Task<ActionResult> DiscountSend([FromBody] OrderWithDiscountEmail order)
        {
            EmailTemplateService<OrderWithDiscountEmail> service = new InvoiceWithDiscountEmailTemplateService();
            var emailService = new EmailService<OrderWithDiscountEmail>(service);
            await emailService.Send(order.Contact.ContactEmail, "Invoice", order);
            return Ok();
        }
    }
}