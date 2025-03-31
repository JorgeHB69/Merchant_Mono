using MediatR;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Business.QueryCommands.Variants.Queries.Queries;

public class GetAllVariantsQuery : IRequest<BaseResponse>;