using MassTransit;
using merchant_api.Business.Services.Email.Bases;
using merchant_api.Business.Services.Email.Concretes;
using merchant_api.Business.Services.Email.Templates;
using merchant_api.Data.Models.Concretes.Emails;

namespace merchant_api.Api.QueueHandlers.MarketingEmailHandlers;

public class PromotionEmailHandler : IConsumer<PromotionEmail>
{
    public async Task Consume(ConsumeContext<PromotionEmail> context)
    {
        var promotionEmail = context.Message;
        EmailTemplateService<PromotionEmail> service = new PromotionEmailTemplateService();
        var emailService = new EmailService<PromotionEmail>(service);
        await emailService.Send(promotionEmail.Contact.ContactEmail, "Promotions", promotionEmail);

    }
}