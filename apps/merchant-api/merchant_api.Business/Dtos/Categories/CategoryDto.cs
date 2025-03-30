namespace merchant_api.Business.Dtos.Categories;

public class CategoryDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public List<SubCategoryDto>? SubCategories { get; set; } = [];
}