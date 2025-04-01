using MassTransit;
using merchant_api.Business.Services.Email.Bases;
using merchant_api.Business.Services.Email.Concretes;
using merchant_api.Business.Services.Email.Templates;
using merchant_api.Data.Models.Concretes.Emails;

namespace merchant_api.Api.QueueHandlers.AdministrationEmailHandler;

public class LowStockEmailHandler : IConsumer<LowStockEmail>
{
    public async Task Consume(ConsumeContext<LowStockEmail> context)
    {
        var lowStockEmail = context.Message;
        EmailTemplateService<LowStockEmail> emailTemplateService = new LowStockEmailTemplaceService();
        var emailService = new EmailService<LowStockEmail>(emailTemplateService);
        await emailService.Send(lowStockEmail.Contact.ContactEmail, "Low stock", lowStockEmail);
    }
}