using MassTransit;
using merchant_api.Business.Services.Email.Bases;
using merchant_api.Business.Services.Email.Concretes;
using merchant_api.Business.Services.Email.Templates.OrdersStatus;
using merchant_api.Data.Models.Concretes.Emails.OrderStatusEmails;
using merchant_api.Data.Models.Interfaces.OrderStatusEmails;

namespace merchant_api.Api.QueueHandlers.OrderEmailHandlers;

public class ConfirmedOrderStatusHandler : IConsumer<ConfirmedEmail>
{
    public async Task Consume(ConsumeContext<ConfirmedEmail> context)
    {
        var confirmedEmail = context.Message;
        EmailTemplateService<IOrderStatusWithProductsEmail> service = new StatusWithProductTemplateService("order-confirmed");
        var emailService = new EmailService<IOrderStatusWithProductsEmail>(service);
        await emailService.Send(confirmedEmail.Contact.ContactEmail, "Order status", confirmedEmail);

    }
}