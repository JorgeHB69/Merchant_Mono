using merchant_api.Business.Services.Email.Bases;
using merchant_api.Business.Utils;
using merchant_api.Data.Models.Interfaces.OrderStatusEmails;

namespace merchant_api.Business.Services.Email.Templates.OrdersStatus
{
    public class StatusWithProductTemplateService : EmailTemplateService<IOrderStatusWithProductsEmail>
    {
        private readonly string htmlPath;

        public StatusWithProductTemplateService(string htmlPath)
        {
            this.htmlPath = $"{htmlPath}.html";
        }

        public override async Task<string> GenerateEmailTemplate(IOrderStatusWithProductsEmail email)
        {
            string templatePath = $"{EmailPath}{htmlPath}";
            string htmlTemplate = await File.ReadAllTextAsync(templatePath);

            htmlTemplate = htmlTemplate.Replace("{{OrderNumber}}", email.OrderNumber);
            htmlTemplate = htmlTemplate.Replace("{{Username}}", email.Contact.ContactName);
            htmlTemplate = htmlTemplate.Replace("{{Items}}", EmailExtraGenerator.GenerateProductItemTemplates(email.Products));

            return htmlTemplate;
        }
    }
}