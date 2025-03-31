using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Images.Commands.Commands;

public class DeleteImageCommand(Guid id) : IRequest<BaseResponse>
{
    public Guid Id { get; set; } = id;
}