using merchant_api.Data.Models.Concretes.Orders;
using merchant_api.Data.Models.Concretes.Users;
using merchant_api.Data.Models.Interfaces;

namespace merchant_api.Data.Models.Concretes.Emails
{
    public class OrderWithDiscountEmail : IEmail
    {
        public OrderWithDiscount Order { get; set; }
        public Contact Contact { get; set; }

        public OrderWithDiscountEmail(Contact contact, OrderWithDiscount order)
        {
            Contact = contact;
            Order = order;
        }
    }
}