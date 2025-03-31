using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Images.Queries.Queries;

public class GetAllImagesQuery(int page, int pageSize) : IRequest<BaseResponse>
{
    public int Page { get; set; } = page;
    public int PageSize { get; set; } = pageSize;
}