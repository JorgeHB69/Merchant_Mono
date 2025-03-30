using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Products.Commands.Commands;

public class UpdateProducrThresholdCommand(Guid productId, int threshold) : IRequest<BaseResponse>
{
    public Guid ProductId { get; set; } = productId;
    public int Threshold { get; set; } = threshold;
}