namespace merchant_api.Business.Services.Email.Interfaces
{
    public interface IEmailTemplateService<in T>
    {
        public Task<string> GenerateEmailTemplate(T email);
    }
}