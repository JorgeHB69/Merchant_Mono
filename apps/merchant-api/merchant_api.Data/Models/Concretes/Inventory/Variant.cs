using merchant_api.Data.Models.Bases;

namespace merchant_api.Data.Models.Concretes.Inventory;

public class Variant : BaseEntity
{ 
    public string Name { get; set; } = string.Empty;
    public ICollection<ProductAttribute> ProductAttributes { get; set; } = new List<ProductAttribute>();    
}