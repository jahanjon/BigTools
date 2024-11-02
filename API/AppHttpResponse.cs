using Common.Enums;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class AppHttpResponse
{
    private readonly ServiceResult _serviceResult;

    public AppHttpResponse(ServiceResult serviceResult)
    {
        _serviceResult = serviceResult;
    }

    public IActionResult Create()
    {
        var statusCode = _serviceResult.StatusCode;
        return statusCode == ApiResultStatusCode.Success ? new OkObjectResult(_serviceResult) : (IActionResult)new OkObjectResult(_serviceResult);
    }
}

public class AppHttpResponse<T>
{
    private readonly ServiceResult<T> _serviceResult;

    public AppHttpResponse(ServiceResult<T> serviceResult)
    {
        _serviceResult = serviceResult;
    }

    public IActionResult Create(object viewModel)
    {
        var statusCode = _serviceResult.StatusCode;
        return statusCode == ApiResultStatusCode.Success
            ? new OkObjectResult(new ServiceResult<object>
            {
                StatusCode = statusCode,
                Message = _serviceResult.Message,
                IsSuccess = true,
                Data = viewModel
            })
            : (IActionResult)new OkObjectResult(new
            {
                IsSuccess = false,
                StatusCode = statusCode,
                message = _serviceResult.Message
            });
    }
}

public class AppHttpResponse<T, TError>
{
    private readonly ServiceResult<T, TError> _serviceResult;

    public AppHttpResponse(ServiceResult<T, TError> serviceResult)
    {
        _serviceResult = serviceResult;
    }

    public IActionResult Create(object viewModel)
    {
        var statusCode = _serviceResult.StatusCode;
        return statusCode == ApiResultStatusCode.Success
            ? new OkObjectResult(new
            {
                StatusCode = statusCode,
                _serviceResult.Message,
                IsSuccess = true,
                Data = viewModel
            })
            : (IActionResult)new OkObjectResult(new
            {
                StatusCode = statusCode,
                _serviceResult.Message,
                IsSuccess = false,
                _serviceResult.Errors
            });
    }
}