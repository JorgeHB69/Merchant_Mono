using MassTransit;
using merchant_api.Business.Services.Email.Bases;
using merchant_api.Business.Services.Email.Concretes;
using merchant_api.Business.Services.Email.Templates;
using merchant_api.Data.Models.Concretes.Emails;

namespace merchant_api.Api.QueueHandlers.MarketingEmailHandlers;

public class ProductRecommendationEmailHandler : IConsumer<ProductRecommendationEmail>
{
    public async Task Consume(ConsumeContext<ProductRecommendationEmail> context)
    {
        var productRecommendationEmail = context.Message;
        EmailTemplateService<ProductRecommendationEmail> service = new ProductRecommendationEmailTemplateService();
        var emailService = new EmailService<ProductRecommendationEmail>(service);
        await emailService.Send(productRecommendationEmail.Contact.ContactEmail, "Product recommendation", productRecommendationEmail);

    }
}