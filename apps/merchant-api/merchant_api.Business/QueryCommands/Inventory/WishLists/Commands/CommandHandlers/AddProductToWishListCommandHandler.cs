using MediatR;
using merchant_api.Business.QueryCommands.WishLists.Commands.Commands;
using merchant_api.Commons.ResponseHandler.Handler.Interfaces;
using merchant_api.Commons.ResponseHandler.Responses.Bases;
using merchant_api.Data.Models.Concretes;
using merchant_api.Data.Models.Concretes.Inventory;
using merchant_api.Data.Repositories.Interfaces;
using merchant_api.Data.Repositories.Interfaces.Inventory;

namespace merchant_api.Business.QueryCommands.WishLists.Commands.CommandHandlers;

public class AddProductToWishListCommandHandler(
    IProductRepository productRepository,
    IWishListRepository wishListRepository,
    IResponseHandlingHelper responseHandlingHelper)
    : IRequestHandler<AddProductToWishListCommand, BaseResponse>
{
    public async Task<BaseResponse> Handle(AddProductToWishListCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId);
        if (product == null)
            return responseHandlingHelper.NotFound<Product>("The product with the follow id " + request.ProductId + " was not found");
        
        var existingWishListEntry = await wishListRepository.GetByUserIdAndProductIdAsync(request.UserId, request.ProductId);
        if (existingWishListEntry != null)
            return responseHandlingHelper.BadRequest<Product>($"The product with the ID {request.ProductId} is already in the wishlist.");

        var wishListEntry = new WishList
        {
            UserId = request.UserId,
            ProductId = request.ProductId,
        };
        
        Console.WriteLine(" pppp " + wishListEntry.UserId + " = " + wishListEntry.ProductId + "\n\n\npppp");

        await wishListRepository.AddAsync(wishListEntry);

        return responseHandlingHelper.Ok(
            $"Product with ID {request.ProductId} was successfully added to the wishlist.", wishListEntry.Id
        );
    }
}