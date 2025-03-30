using merchant_api.Data.Models.Bases;

namespace merchant_api.Data.Models.Concretes;

public class Image : BaseEntity
{
    public Guid? ProductId { get; set; }
    public string AltText { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public Product? Product { get; set; }
    public ProductVariant? ProductVariant { get; set; } 
}