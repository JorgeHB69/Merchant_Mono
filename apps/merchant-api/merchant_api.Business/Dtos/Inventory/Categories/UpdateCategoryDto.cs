namespace merchant_api.Business.Dtos.Inventory.Categories;

public class UpdateCategoryDto
{
    public Guid Id { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public string? Name {get; set;}
    public bool? IsActive { get; set; }
}