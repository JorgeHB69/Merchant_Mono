using merchant_api.Data.Models.Concretes.Users;
using merchant_api.Data.Models.Interfaces;

namespace merchant_api.Data.Models.Concretes.Emails
{
    public class NewUserEmail : IEmail
    {
        public string UserType { get; set; }
        public string StoreName { get; set; }
        public string AdminPanelUrl { get; set; }
        public string StoreEmail { get; set; }
        public List<string> Responsibilities { get; set; }

        public Contact Contact { get; set; }

        public NewUserEmail(
            Contact contact,
            string userType,
            string storeName,
            string adminPanelUrl,
            List<string> responsibilities,
            string storeEmail)
        {
            UserType = userType;
            StoreName = storeName;
            StoreEmail = storeEmail;
            AdminPanelUrl = adminPanelUrl;
            Responsibilities = responsibilities;
            Contact = contact;
        }
    }
}