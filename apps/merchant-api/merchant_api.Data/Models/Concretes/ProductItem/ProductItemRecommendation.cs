using merchant_api.Data.Models.Interfaces;

namespace merchant_api.Data.Models.Concretes.ProductItem
{
    public class ProductItemRecommendation : IProductItem
    {
        public string ProductUrl { get; set; }
        public string ImageUrl { get; set; }
        public string ProductName { get; set; }

        public ProductItemRecommendation(string imageUrl, string productName, string productUrl)
        {
            ImageUrl = imageUrl;
            ProductName = productName;
            ProductUrl = productUrl;
        }
    }
}