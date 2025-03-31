using MediatR;
using merchant_api.Business.Dtos.Inventory.Categories;
using merchant_api.Business.QueryCommands.Categories.Queries.Queries;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Models.Concretes.Inventory;
using merchant_api.Data.Repositories.Interfaces;

namespace merchant_api.Business.QueryCommands.Categories.Queries.QueriesHandlers;

public class GetAllCategoriesQueryHandler(IRepository<Category> categoryRepository, IResponseHandlingHelper responseHandlingHelper)
    : IRequestHandler<GetAllCategoriesQuery, BaseResponse>
{
    public async Task<BaseResponse> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var totalCategories = await categoryRepository.GetAllAsync();
        Dictionary<Guid, List<Category>> subcategories = [];
        List<Category> categories = [];

        foreach (var category in totalCategories)
        {
            if (category.ParentCategoryId == null)
            {
                categories.Add(category);
            }
            else
            {
                if (subcategories.TryGetValue(category.ParentCategoryId.Value, out var value))
                {
                    value.Add(category);
                }
                else
                {

                    subcategories[category.ParentCategoryId.Value] = [category];
                }
            }
        }

        return responseHandlingHelper.Ok("Categories have been successfully obtained.", 
            (from category in categories
                let subcats = subcategories.ContainsKey(category.Id) 
                    ? subcategories[category.Id]
                        .Select(sc => new SubCategoryDto { Name = sc.Name, Id = sc.Id })
                        .ToList()
                    : new List<SubCategoryDto>()
                select new CategoryDto { 
                    Name = category.Name, 
                    Id = category.Id, 
                    SubCategories = subcats 
                }).ToList());
        
    }
}