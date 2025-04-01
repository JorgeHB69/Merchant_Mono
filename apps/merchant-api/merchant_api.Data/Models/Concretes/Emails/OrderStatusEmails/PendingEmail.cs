using merchant_api.Data.Models.Concretes.Users;
using merchant_api.Data.Models.Interfaces;
using merchant_api.Data.Models.Interfaces.OrderStatusEmails;

namespace merchant_api.Data.Models.Concretes.Emails.OrderStatusEmails
{
    public class PendingEmail : IOrderStatusWithProductsEmail
    {
        public PendingEmail(Contact contact, string orderNumber, List<IProductItem> products)
        {
            Contact = contact;
            OrderNumber = orderNumber;
            Products = products;
        }
        public Contact Contact { get; set; }
        public string OrderNumber { get; set; }
        public List<IProductItem> Products { get; set; }
    }
}