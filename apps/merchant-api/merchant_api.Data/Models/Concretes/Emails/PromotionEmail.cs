using merchant_api.Data.Models.Concretes.ProductItem;
using merchant_api.Data.Models.Concretes.Users;
using merchant_api.Data.Models.Interfaces;

namespace merchant_api.Data.Models.Concretes.Emails
{
    public class PromotionEmail : IEmail
    {
        public List<ProductItemPromotion> Products { get; set; }

        public Contact Contact { get; set; }

        public PromotionEmail(Contact contact, List<ProductItemPromotion> products)
        {
            Contact = contact;
            Products = products;
        }
    }
}