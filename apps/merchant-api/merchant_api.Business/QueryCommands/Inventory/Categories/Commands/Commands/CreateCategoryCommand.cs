using MediatR;
using merchant_api.Business.Dtos.Inventory.Categories;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Categories.Commands.Commands;

public class CreateCategoryCommand(CreateCategoryDto createCategoryDto) : IRequest<BaseResponse>
{
    public CreateCategoryDto CreateCategoryDto { get; set; } = createCategoryDto;
}