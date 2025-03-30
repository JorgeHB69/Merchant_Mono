using System.Collections.Generic;
using merchant_api.Commons.ResponseHandler.Responses.Concretes;

namespace merchant_api.Commons.ResponseHandler.Handler.Interfaces;

public interface IResponseHandlingHelper
{
    public SuccessResponse<T> Ok<T>(string message, T data);
    public SuccessResponse<T> Created<T>(string message, T data);
    public ErrorResponse NotFound<T>(string message);
    public ErrorResponse BadRequest<T>(string message, List<string>? errors = null);
    public ErrorResponse InternalServerError<T>(string message);
}