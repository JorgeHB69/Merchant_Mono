using merchant_api.Business.Dtos.Inventory.ProductVariants;

namespace merchant_api.Business.Dtos.Inventory.Products;

public class CreateProductDto
{
    public Guid StoreId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Brand { get; set; } = string.Empty;
     public int? LowStockThreshold { get; set; }
    public List<Guid> CategoryIds { get; set; } = [];
    public List<ProductVariantImageDto> Images { get; set; } = [];
    public List<ProductVariantForCreateDto> ProductVariants { get; set; } = [];
}