using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Products.Commands.Commands;

public class DeleteProductCommand(Guid id) : IRequest<BaseResponse>
{
    public Guid Id { get; set; } = id;
}