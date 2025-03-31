using MediatR;
using merchant_api.Business.Dtos.Inventory.Variants;
using merchant_api.Business.QueryCommands.Variants.Queries.Queries;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Models.Concretes.Inventory;
using merchant_api.Data.Repositories.Interfaces;

namespace merchant_api.Business.QueryCommands.Variants.Queries.QueryHandlers;

public class GetVariantByIdQueryHandler(IRepository<Variant> variantRepository, IResponseHandlingHelper responseHandlingHelper)
    : IRequestHandler<GetVariantByIdQuery, BaseResponse>
{
    public async Task<BaseResponse> Handle(GetVariantByIdQuery request, CancellationToken cancellationToken)
    {
        var variant = await variantRepository.GetByIdAsync(request.Id);
        if (variant == null)
            return responseHandlingHelper.NotFound<VariantDto>("The variant with the follow id " + request.Id + " was not found");
        
        return responseHandlingHelper.Ok("The category has been successfully obtained.",  new VariantDto
        {
            Id = variant!.Id, 
            Name = variant.Name, 
            IsActive = variant.IsActive, 
            values = variant.ProductAttributes.Select(attribute => new ValueDto { Id = attribute.Id, Name = attribute.Value }).ToList()
        });
    }
}