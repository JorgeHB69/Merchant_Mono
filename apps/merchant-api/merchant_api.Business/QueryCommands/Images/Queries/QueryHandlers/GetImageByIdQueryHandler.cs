using MediatR;
using merchant_api.Business.Dtos.Images;
using merchant_api.Business.QueryCommands.Images.Queries.Queries;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Repositories.Interfaces;

namespace merchant_api.Business.QueryCommands.Images.Queries.QueryHandlers;

public class GetImageByIdQueryHandler(IRepository<Image> imageRepository, IResponseHandlingHelper responseHandlingHelper)
    : IRequestHandler<GetImageByIdQuery, BaseResponse>
{
    public async Task<BaseResponse> Handle(GetImageByIdQuery request, CancellationToken cancellationToken)
    {
        var image = await imageRepository.GetByIdAsync(request.Id);
        if (image == null)
            return responseHandlingHelper.NotFound<ImageDto>("The image with the follow id " + request.Id + " was not found");
        
        return responseHandlingHelper.Ok("The image has been successfully obtained.", new ImageDto
        {
            ImageId = image!.Id,
            ProductId = image.ProductId,
            Url = image.Url,
            AltText = image.AltText,
            IsActive = image.IsActive
        });
    }
}