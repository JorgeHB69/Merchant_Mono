using MediatR;
using merchant_api.Business.Dtos.Categories;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Categories.Commands.Commands;

public class UpdateCategoryCommand(UpdateCategoryDto categoryDto) : IRequest<BaseResponse>
{
    public UpdateCategoryDto CategoryDto { get; } = categoryDto;
}