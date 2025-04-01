using MassTransit;
using merchant_api.Business.Services.Email.Bases;
using merchant_api.Business.Services.Email.Concretes;
using merchant_api.Business.Services.Email.Templates;
using merchant_api.Data.Models.Concretes.Emails;

namespace merchant_api.Api.QueueHandlers.MerchantEmailHandlers;

public class NewUserEmailHandler : IConsumer<NewUserEmail>
{
    public async Task Consume(ConsumeContext<NewUserEmail> context)
    {
        var newUserEmail = context.Message;
        EmailTemplateService<NewUserEmail> service = new NewUserEmailTemplateService();
        var emailService = new EmailService<NewUserEmail>(service);
        await emailService.Send(newUserEmail.Contact.ContactEmail, "New User", newUserEmail);
    }
}