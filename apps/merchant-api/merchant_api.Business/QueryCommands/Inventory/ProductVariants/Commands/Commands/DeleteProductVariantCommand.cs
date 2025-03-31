using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.ProductVariants.Commands.Commands;

public class DeleteProductVariantCommand(Guid id) : IRequest<BaseResponse>
{
    public Guid Id { get; set; } = id;
}