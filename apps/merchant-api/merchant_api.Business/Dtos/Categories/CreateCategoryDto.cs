namespace merchant_api.Business.Dtos.Categories;

public class CreateCategoryDto
{
    public Guid? ParentCategoryId { get; set; }
    public string Name {get; set;} = string.Empty;
}