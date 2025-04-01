using merchant_api.Business.Services.Email.Interfaces;

namespace merchant_api.Business.Services.Email.Bases
{
    public abstract class EmailTemplateService<T> : IEmailTemplateService<T>
    {
        protected const string EmailPath = "templates/";

        public abstract Task<string> GenerateEmailTemplate(T email);
    }
}