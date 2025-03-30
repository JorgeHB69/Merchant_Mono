using merchant_api.Business.Dtos.ProductVariants;

namespace merchant_api.Business.Dtos.Products;

public class ProductVariantForCreateDto
{
    public ProductVariantImageDto? Image { get; set; }
    public double PriceAdjustment { get; set; }
    public int StockQuantity { get; set; }
    public List<ProductVariantAttributeDto> Attributes { get; set; } = [];
}