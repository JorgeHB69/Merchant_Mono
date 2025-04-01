using merchant_api.Data.Models.Concretes.Users;
using merchant_api.Data.Models.Interfaces;

namespace merchant_api.Data.Models.Concretes.Emails
{
    public class WelcomeEmail : IEmail
    {
        public Contact Contact { get; set; }

        public WelcomeEmail(Contact contact)
        {
            Contact = contact;
        }
    }
}