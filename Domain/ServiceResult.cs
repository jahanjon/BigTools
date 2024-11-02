using Common.Enums;
using Common.Utilities;
using Newtonsoft.Json;

namespace Domain;

public class ServiceResult
{
    public ServiceResult(string message = null)
    {
        IsSuccess = true;
        StatusCode = ApiResultStatusCode.Success;
        Message = message ?? ApiResultStatusCode.Success.ToDisplay();
    }

    public ServiceResult(bool isSuccess, ApiResultStatusCode statusCode, string message = null)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message ?? statusCode.ToDisplay();
    }

    public bool IsSuccess { get; set; }
    public ApiResultStatusCode StatusCode { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Message { get; set; }
}

public class ServiceResult<TData> : ServiceResult
{
    public ServiceResult()
    {
    }

    public ServiceResult(TData data, string message = null)
    {
        Data = data;
        IsSuccess = true;
        StatusCode = ApiResultStatusCode.Success;
        Message = message ?? ApiResultStatusCode.Success.ToDisplay();
    }

    public ServiceResult(bool isSuccess, ApiResultStatusCode statusCode, TData data, string message = null)
        : base(isSuccess, statusCode, message)
    {
        Data = data;
    }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public TData Data { get; set; }
}

public class ServiceResult<TData, TError> : ServiceResult<TData>
{
    public ServiceResult()
    {
        Data = default;
        IsSuccess = true;
        StatusCode = ApiResultStatusCode.Success;
        Message = ApiResultStatusCode.Success.ToDisplay();
    }

    public ServiceResult(string message)
    {
        Data = default;
        IsSuccess = true;
        StatusCode = ApiResultStatusCode.Success;
        Message = message ?? ApiResultStatusCode.Success.ToDisplay();
    }

    public ServiceResult(TData data)
    {
        Data = data;
        IsSuccess = true;
        StatusCode = ApiResultStatusCode.Success;
        Message = ApiResultStatusCode.Success.ToDisplay();
    }

    public ServiceResult(TError errors)
    {
        Errors = errors;
        IsSuccess = false;
        StatusCode = ApiResultStatusCode.BadRequest;
        Message = ApiResultStatusCode.BadRequest.ToDisplay();
    }

    public ServiceResult(bool isSuccess, ApiResultStatusCode statusCode, TData data, TError errors,
        string message = null) : base(isSuccess, statusCode, data, message)
    {
        Errors = errors;
    }

    public TError Errors { get; set; }
}