namespace merchant_api.Business.Dtos.Inventory.ProductVariants;

public class GetProductVariantAttributeDto
{
    public Guid ProductVariantAttributeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}