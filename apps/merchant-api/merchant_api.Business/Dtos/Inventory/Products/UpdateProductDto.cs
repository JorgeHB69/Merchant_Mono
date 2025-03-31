using merchant_api.Business.Dtos.Inventory.Images;
using merchant_api.Business.Dtos.Inventory.ProductVariants;

namespace merchant_api.Business.Dtos.Inventory.Products;

public class UpdateProductDto
{
    public Guid StoreId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Brand { get; set; } = string.Empty;
    public List<Guid> CategoryIds { get; set; } = [];
    public List<UpdateImageDto> Images { get; set; } = [];
    public List<UpdateProductVariantDto> ProductVariants { get; set; } = [];
}