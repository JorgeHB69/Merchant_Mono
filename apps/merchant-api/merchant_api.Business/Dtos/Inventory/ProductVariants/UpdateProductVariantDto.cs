namespace merchant_api.Business.Dtos.Inventory.ProductVariants;

public class UpdateProductVariantDto
{
    public Guid? Id { get; set; }
    public ProductVariantImageDto? Image { get; set; }
    public double? PriceAdjustment { get; set; }
    public int? StockQuantity { get; set; }
    public ICollection<ProductVariantAttributeDto>? Attributes { get; set; }
}