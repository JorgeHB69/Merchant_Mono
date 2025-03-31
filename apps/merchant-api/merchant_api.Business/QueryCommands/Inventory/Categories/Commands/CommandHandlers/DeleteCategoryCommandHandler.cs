using MediatR;
using merchant_api.Business.QueryCommands.Categories.Commands.Commands;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Models.Concretes.Inventory;
using merchant_api.Data.Repositories.Interfaces;

namespace merchant_api.Business.QueryCommands.Categories.Commands.CommandHandlers;

public class DeleteCategoryCommandHandler(IRepository<Category> categoryRepository, IResponseHandlingHelper responseHandlingHelper) : 
    IRequestHandler<DeleteCategoryCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.Id);
        if (category == null) return responseHandlingHelper.NotFound<Category>($"The category with the follow id '{request.Id}' was not found.");

        var response = await categoryRepository.DeleteAsync(request.Id);
        return responseHandlingHelper.Ok("The category has been successfully deleted.", response);
    }
}