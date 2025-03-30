using System.Collections.Generic;
using merchant_api.Commons.ResponseHandler.Responses.Bases;

namespace merchant_api.Commons.ResponseHandler.Responses.Concretes;

public class ErrorResponse : BaseResponse
{
    public List<string> Errors { get; set; }

    public ErrorResponse(int statusCode, string message, List<string>? errors = null)
    {
        IsSuccess = false;
        StatusCode = statusCode;
        Message = message;
        Errors = errors ?? [];
    }
}