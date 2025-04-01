using MassTransit;
using merchant_api.Business.Services.Email.Bases;
using merchant_api.Business.Services.Email.Concretes;
using merchant_api.Business.Services.Email.Templates;
using merchant_api.Data.Models.Concretes.Emails;

namespace merchant_api.Api.QueueHandlers.OrderEmailHandlers;

public class OrderEmailHandler : IConsumer<OrderEmail>
{
    public async Task Consume(ConsumeContext<OrderEmail> context)
    {
        var orderEmail = context.Message;
        EmailTemplateService<OrderEmail> service = new InvoiceEmailTemplateService();
        var emailService = new EmailService<OrderEmail>(service);
        await emailService.Send(orderEmail.Contact.ContactEmail, "Invoice", orderEmail);

    }
}