using MassTransit;
using merchant_api.Business.Services.Email.Bases;
using merchant_api.Business.Services.Email.Concretes;
using merchant_api.Business.Services.Email.Templates;
using merchant_api.Data.Models.Concretes.Emails;

namespace merchant_api.Api.QueueHandlers.MerchantEmailHandlers;

public class WelcomeEmailHandler : IConsumer<WelcomeEmail>
{
    public async Task Consume(ConsumeContext<WelcomeEmail> context)
    {
        var welcomeEmail = context.Message;
        EmailTemplateService<WelcomeEmail> service = new WelcomeEmailTemplateService();
        var emailService = new EmailService<WelcomeEmail>(service);
        await emailService.Send(welcomeEmail.Contact.ContactEmail, "Welcome", welcomeEmail);
    }
}