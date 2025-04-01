using merchant_api.Business.Services.Email.Bases;
using merchant_api.Data.Models.Concretes.Emails;

namespace merchant_api.Business.Services.Email.Templates
{
    public class WelcomeEmailTemplateService : EmailTemplateService<WelcomeEmail>
    {
        public override async Task<string> GenerateEmailTemplate(WelcomeEmail email)
        {
            string templatePath = $"{EmailPath}welcome.html";
            string htmlTemplate = await File.ReadAllTextAsync(templatePath);

            htmlTemplate = htmlTemplate.Replace("{{Username}}", email.Contact.ContactName);

            return htmlTemplate;
        }
    }
}