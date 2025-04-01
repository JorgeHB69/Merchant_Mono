using merchant_api.Data.Models.Concretes.Orders;
using merchant_api.Data.Models.Concretes.Users;
using merchant_api.Data.Models.Interfaces;

namespace merchant_api.Data.Models.Concretes.Emails
{
    public class LowStockEmail : IEmail
    {
        public List<OrderItem> OrderItems { get; set; }
        public string InventoryUrl { get; set; }

        public Contact Contact { get; set; }

        public LowStockEmail(Contact contact, List<OrderItem> orderItems, string inventoryUrl)
        {
            OrderItems = orderItems;
            InventoryUrl = inventoryUrl;
            Contact = contact;
        }
    }
}