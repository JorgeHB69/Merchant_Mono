using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Categories.Commands.Commands;

public class DeleteCategoryCommand(Guid id) : IRequest<BaseResponse>
{
    public Guid Id { get; set; } = id;
}