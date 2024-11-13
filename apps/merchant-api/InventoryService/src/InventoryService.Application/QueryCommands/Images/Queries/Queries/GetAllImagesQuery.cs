using InventoryService.Application.Dtos;
using InventoryService.Application.Dtos.Images;
using InventoryService.Commons.ResponseHandler.Responses.Bases;
using InventoryService.Domain.Concretes;
using MediatR;

namespace InventoryService.Application.QueryCommands.Images.Queries.Queries;

public class GetAllImagesQuery(int page, int pageSize) : IRequest<BaseResponse>
{
    public int Page { get; set; } = page;
    public int PageSize { get; set; } = pageSize;
}