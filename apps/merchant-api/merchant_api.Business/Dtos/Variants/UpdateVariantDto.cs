namespace merchant_api.Business.Dtos.Variants;

public class UpdateVariantDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public bool? IsActive { get; set; }
}