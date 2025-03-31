namespace merchant_api.Business.Dtos.Inventory.Variants;

public class UpdateVariantDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public bool? IsActive { get; set; }
}