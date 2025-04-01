using merchant_api.Data.Models.Concretes.Users;
using merchant_api.Data.Models.Interfaces.OrderStatusEmails;

namespace merchant_api.Data.Models.Concretes.Emails.OrderStatusEmails
{
    public class ShippedEmail : IOrderStatusWithTimeEmail
    {
        public ShippedEmail(Contact contact, string orderNumber)
        {
            Contact = contact;
            OrderNumber = orderNumber;
            Time = DateTime.Now;
        }
        
        public Contact Contact { get; set; }
        public string OrderNumber { get; set; }
        public DateTime Time { get; set; }
    }
}