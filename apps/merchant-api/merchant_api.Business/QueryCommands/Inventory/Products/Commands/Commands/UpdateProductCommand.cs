using MediatR;
using merchant_api.Business.Dtos.Inventory.Products;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Products.Commands.Commands;

public class UpdateProductCommand(Guid id, UpdateProductDto productDto) : IRequest<BaseResponse>
{
    public Guid Id { get; set; } = id;
    public UpdateProductDto ProductDto { get; set; } = productDto;
}