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

public class CreateCategoryCommandHandler(
    IRepository<Category> categoryRepository, 
    IResponseHandlingHelper responseHandlingHelper,
    IValidator<CreateCategoryDto> validator) : 
    IRequestHandler<CreateCategoryCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryDto = request.CreateCategoryDto;
        var response = await validator.ValidateAsync(categoryDto, cancellationToken);
        if (!response.IsValid) return responseHandlingHelper.BadRequest<CreateCategoryDto>(
            "The operation to create a category was not completed, please check the errors.", 
            response.Errors.Select(e => e.ErrorMessage).ToList());
        
        var category = new Category
        {
            Name = categoryDto.Name,
            ParentCategoryId = categoryDto.ParentCategoryId
        };
        
        category = await categoryRepository.AddAsync(category);
        return responseHandlingHelper.Created("The category was added successfully.", category.Id);
    }
}