using merchant_api.Data.Models.Concretes.ProductItem;
using merchant_api.Data.Models.Concretes.Users;
using merchant_api.Data.Models.Interfaces;

namespace merchant_api.Data.Models.Concretes.Emails
{
    public class ProductRecommendationEmail : IEmail
    {
        public List<ProductItemRecommendation> Products { get; set; }

        public ProductRecommendationEmail(Contact contact, List<ProductItemRecommendation> products)
        {
            Contact = contact;
            Products = products;
        }

        public Contact Contact { get; set; }
    }
}