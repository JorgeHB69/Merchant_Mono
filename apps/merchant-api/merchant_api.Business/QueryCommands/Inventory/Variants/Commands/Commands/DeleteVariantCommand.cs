using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Variants.Commands.Commands;

public class DeleteVariantCommand(Guid id) : IRequest<BaseResponse>
{
    public Guid Id { get; set; } = id;
}