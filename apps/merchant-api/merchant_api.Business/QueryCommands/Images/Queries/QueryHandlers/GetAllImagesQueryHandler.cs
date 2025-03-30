using MediatR;
using merchant_api.Business.Dtos;
using merchant_api.Business.Dtos.Images;
using merchant_api.Business.QueryCommands.Images.Queries.Queries;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Repositories.Interfaces;

namespace merchant_api.Business.QueryCommands.Images.Queries.QueryHandlers;

public class GetAllImagesQueryHandler(IRepository<Image> imageRepository, IResponseHandlingHelper responseHandlingHelper)
    : IRequestHandler<GetAllImagesQuery, BaseResponse>
{
    public async Task<BaseResponse> Handle(GetAllImagesQuery request, CancellationToken cancellationToken)
    {
        var totalImages = await imageRepository.GetAllAsync(request.Page, request.PageSize);
        var totalImagesDto = totalImages.Select(image => new ImageDto
        {
            ImageId = image.Id,
            ProductId = image.ProductId,
            Url = image.Url,
            AltText = image.AltText,
            IsActive = image.IsActive
        }).ToList();
        int totalItems = await imageRepository.GetCountAsync();

        var imagesToDisplay = new PaginatedResponseDto<ImageDto>(totalImagesDto, totalItems, request.Page, request.PageSize);

        return responseHandlingHelper.Ok("Images have been successfully obtained.", imagesToDisplay);
    }
}