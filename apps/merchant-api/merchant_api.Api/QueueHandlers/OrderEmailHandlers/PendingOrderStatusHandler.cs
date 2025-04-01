using MassTransit;
using merchant_api.Business.Services.Email.Bases;
using merchant_api.Business.Services.Email.Concretes;
using merchant_api.Business.Services.Email.Templates.OrdersStatus;
using merchant_api.Data.Models.Concretes.Emails.OrderStatusEmails;
using merchant_api.Data.Models.Interfaces.OrderStatusEmails;

namespace merchant_api.Api.QueueHandlers.OrderEmailHandlers;

public class PendingOrderStatusHandler : IConsumer<PendingEmail>
{
    public async Task Consume(ConsumeContext<PendingEmail> context)
    {
        var pendingEmail = context.Message;
        EmailTemplateService<IOrderStatusWithProductsEmail> service = new StatusWithProductTemplateService("order-pending");
        var emailService = new EmailService<IOrderStatusWithProductsEmail>(service);
        await emailService.Send(pendingEmail.Contact.ContactEmail, "Order status", pendingEmail);

    }

}