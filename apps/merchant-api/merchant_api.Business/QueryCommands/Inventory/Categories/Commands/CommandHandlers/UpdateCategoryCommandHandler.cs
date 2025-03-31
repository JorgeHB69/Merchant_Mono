using FluentValidation;
using MediatR;
using merchant_api.Business.Dtos.Inventory.Categories;
using merchant_api.Business.QueryCommands.Categories.Commands.Commands;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Models.Concretes.Inventory;
using merchant_api.Data.Repositories.Interfaces;

namespace merchant_api.Business.QueryCommands.Categories.Commands.CommandHandlers;

public class UpdateCategoryCommandHandler(
    IRepository<Category> categoryRepository, 
    IResponseHandlingHelper responseHandlingHelper,
    IValidator<UpdateCategoryDto> validator) : IRequestHandler<UpdateCategoryCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryDto = request.CategoryDto;
        var categoryToUpdate = await categoryRepository.GetByIdAsync(categoryDto.Id);
        if (categoryToUpdate == null) return responseHandlingHelper.NotFound<Category>(
            $"The category with the follow id '{categoryDto.Id}' was not found.");
        
        var response = await validator.ValidateAsync(categoryDto, cancellationToken);
        if (!response.IsValid) return responseHandlingHelper.BadRequest<CreateCategoryDto>(
            "The operation to update the category was not completed, please check the errors.", 
            response.Errors.Select(e => e.ErrorMessage).ToList());
        
        categoryToUpdate.Name = categoryDto.Name ?? categoryToUpdate.Name;
        categoryToUpdate.ParentCategoryId = categoryDto.ParentCategoryId ?? categoryToUpdate.ParentCategoryId;
        categoryToUpdate.IsActive = categoryDto.IsActive ?? categoryToUpdate.IsActive;
        await categoryRepository.UpdateAsync(categoryToUpdate);
        
        var subcategories = categoryToUpdate.SubCategories.Select(subCategory => 
            new SubCategoryDto { Id = subCategory.Id, Name = subCategory.Name }).ToList();
        
        var categoryToDisplay = 
            new CategoryDto { Id = categoryToUpdate.Id, Name = categoryToUpdate.Name, SubCategories = subcategories };
        
        return responseHandlingHelper.Ok("The category has been successfully updated.", categoryToDisplay);
    }
}