using merchant_api.Commons.ResponseHandler.Responses.Concretes;

namespace merchant_api.Commons.ResponseHandler.Responses;

public class ResponseFactory<T>
{
    public static SuccessResponse<T> CreateSuccess(int statusCode, string message, T data)
    {
        return new SuccessResponse<T>(statusCode, message, data);
    }

    public static ErrorResponse CreateError(int statusCode, string message, List<string>? errors = null)
    {
        return new ErrorResponse(statusCode, message, errors);
    }
}