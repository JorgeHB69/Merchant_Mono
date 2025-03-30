using FluentValidation;
using MediatR;
using merchant_api.Business.Dtos.Images;
using merchant_api.Business.QueryCommands.Images.Commands.Commands;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Repositories.Interfaces;

namespace merchant_api.Business.QueryCommands.Images.Commands.CommandHandlers;

public class UpdateImageCommandHandler(
    IValidator<UpdateImageDto> validator,
    IRepository<Image> imageRepository, 
    IResponseHandlingHelper responseHandlingHelper) : IRequestHandler<UpdateImageCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(UpdateImageCommand request, CancellationToken cancellationToken)
    {
        var imageDto = request.Image;
        var imageToUpdate = await imageRepository.GetByIdAsync(imageDto.Id);        
        if (imageToUpdate == null) return responseHandlingHelper.NotFound<Image>($"The image with the follow id '{imageDto.Id}' was not found.");        
        
        var response = await validator.ValidateAsync(imageDto, cancellationToken);
        if (!response.IsValid) return responseHandlingHelper.BadRequest<UpdateImageDto>(
            "The operation to update the image was not completed, please check the errors.", 
            response.Errors.Select(e => e.ErrorMessage).ToList());
        
        imageToUpdate.Url = imageDto.Url ?? imageToUpdate.Url;
        imageToUpdate.AltText = imageDto.AltText ?? imageToUpdate.AltText;
        imageToUpdate.IsActive = imageDto.IsActive ?? imageToUpdate.IsActive;
        
        await imageRepository.UpdateAsync(imageToUpdate);
        var imageToDisplay = new ImageDto
        {
            ImageId = imageToUpdate.Id,
            ProductId = imageToUpdate.ProductId,
            Url = imageToUpdate.Url,
            AltText = imageToUpdate.AltText,
            IsActive = imageToUpdate.IsActive
        };
        return responseHandlingHelper.Ok("The image has been successfully updated.", imageToDisplay);

    }
}