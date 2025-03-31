namespace merchant_api.Business.Dtos.Inventory.Images;

public class UpdateImageDto
{
    public Guid Id { get; set; }
    public string? AltText { get; set; }
    public string? Url { get; set; }
    public bool? IsActive { get; set; }
}