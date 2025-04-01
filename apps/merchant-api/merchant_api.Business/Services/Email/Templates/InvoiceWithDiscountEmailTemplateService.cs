using merchant_api.Business.Services.Email.Bases;
using merchant_api.Business.Utils;
using merchant_api.Data.Models.Concretes.Emails;

namespace merchant_api.Business.Services.Email.Templates
{
    public class InvoiceWithDiscountEmailTemplateService : EmailTemplateService<OrderWithDiscountEmail>
    {
        public override async Task<string> GenerateEmailTemplate(OrderWithDiscountEmail email)
        {
            string templatePath = $"{EmailPath}invoice-with-discount.html";

            string htmlTemplate = await File.ReadAllTextAsync(templatePath);

            htmlTemplate = htmlTemplate.Replace("{{OrderNumber}}", email.Order.OrderNumber);
            htmlTemplate = htmlTemplate.Replace("{{Items}}", EmailExtraGenerator.GenerateTableInvoice(email.Order.OrderItems));
            htmlTemplate = htmlTemplate.Replace("{{AfterPrice}}", $"{email.Order.OrderTotal}");
            htmlTemplate = htmlTemplate.Replace("{{Price}}", $"{email.Order.OrderFinalTotal}");
            htmlTemplate = htmlTemplate.Replace("{{Discount}}", $"{email.Order.DiscountPercentage}");

            return htmlTemplate;
        }   
    }
}