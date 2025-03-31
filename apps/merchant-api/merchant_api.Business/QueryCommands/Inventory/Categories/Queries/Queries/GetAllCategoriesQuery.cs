using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Categories.Queries.Queries;

public class GetAllCategoriesQuery : IRequest<BaseResponse>;