using MassTransit;
using merchant_api.Business.Services.Email.Bases;
using merchant_api.Business.Services.Email.Concretes;
using merchant_api.Business.Services.Email.Templates.OrdersStatus;
using merchant_api.Data.Models.Concretes.Emails.OrderStatusEmails;
using merchant_api.Data.Models.Interfaces.OrderStatusEmails;

namespace merchant_api.Api.QueueHandlers.OrderEmailHandlers;

public class CanceledOrderStatusHandler : IConsumer<CanceledEmail>
{
    public async Task Consume(ConsumeContext<CanceledEmail> context)
    {
        var canceledEmail = context.Message;
        EmailTemplateService<IOrderStatusWithProductsEmail> service = new StatusWithProductTemplateService("order-canceled");
        var emailService = new EmailService<IOrderStatusWithProductsEmail>(service);
        await emailService.Send(canceledEmail.Contact.ContactEmail, "Order status", canceledEmail);

    }
}
