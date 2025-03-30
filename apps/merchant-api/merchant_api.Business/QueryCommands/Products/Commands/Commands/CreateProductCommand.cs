using MediatR;
using merchant_api.Business.Dtos.Products;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Products.Commands.Commands;

public class CreateProductCommand(CreateProductDto productDto) : IRequest<BaseResponse>
{
    public CreateProductDto ProductDto { get; } = productDto;
}