using MediatR;
using merchant_api.Business.Dtos.Variants;
using merchant_api.Business.QueryCommands.Variants.Queries.Queries;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Repositories.Interfaces;

namespace merchant_api.Business.QueryCommands.Variants.Queries.QueryHandlers;

public class GetAllVariantsQueryHandler(IRepository<Variant> variantRepository, IResponseHandlingHelper responseHandlingHelper)
    : IRequestHandler<GetAllVariantsQuery, BaseResponse>
{
    public async Task<BaseResponse> Handle(GetAllVariantsQuery request, CancellationToken cancellationToken)
    {
        var totalVariants = await variantRepository.GetAllAsync();

        return responseHandlingHelper.Ok("Variants have been successfully obtained.", totalVariants.Select(
            variant => new VariantDto
            {
                Id = variant.Id, 
                Name = variant.Name, 
                IsActive = variant.IsActive, 
                values = variant.ProductAttributes.Select(attribute => new ValueDto { Id = attribute.Id, Name = attribute.Value }).ToList()
            }).ToList());
    }
}