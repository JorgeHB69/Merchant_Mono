using MediatR;
using merchant_api.Business.Dtos.Inventory.Categories;
using merchant_api.Business.QueryCommands.Categories.Queries.Queries;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Models.Concretes.Inventory;
using merchant_api.Data.Repositories.Interfaces;

namespace merchant_api.Business.QueryCommands.Categories.Queries.QueriesHandlers;

public class GetCategoryByIdQueryHandler(
    IRepository<Category> categoryRepository, 
    IResponseHandlingHelper responseHandlingHelper)
: IRequestHandler<GetCategoryByIdQuery, BaseResponse>
{
    public async Task<BaseResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.Id);
        if (category == null)
            return responseHandlingHelper.NotFound<CategoryDto>("The category with the follow id " + request.Id + " was not found");
        
        var subcategories = category.SubCategories.Select(subCategory => new SubCategoryDto
        {
            Id = subCategory.Id,
            Name = subCategory.Name,
        }).ToList();

        return responseHandlingHelper.Ok("The category has been successfully obtained.", new CategoryDto
        {
            Name = category.Name,
            Id = category?.Id,
            SubCategories = subcategories
        }); 
    }
}