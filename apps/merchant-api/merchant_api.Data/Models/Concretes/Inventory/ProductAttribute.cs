using merchant_api.Data.Models.Bases;

namespace merchant_api.Data.Models.Concretes.Inventory;

public class ProductAttribute : BaseEntity
{
    public Guid ProductVariantId { get; set; }
    public Guid VariantId { get; set; }
    public string Value { get; set; } = string.Empty;
    public ProductVariant? ProductVariant { get; set; }
    public Variant? Variant { get; set; }
}