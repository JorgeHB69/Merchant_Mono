using merchant_api.Data.Models.Concretes.Orders;
using merchant_api.Data.Models.Concretes.Users;
using merchant_api.Data.Models.Interfaces;

namespace merchant_api.Data.Models.Concretes.Emails
{
    public class OrderEmail : IEmail
    {
        public OrderNormal Order { get; set; }
        public Contact Contact { get; set; }

        public OrderEmail(Contact contact, OrderNormal order)
        {
            Contact = contact;
            Order = order;
        }
    }
}