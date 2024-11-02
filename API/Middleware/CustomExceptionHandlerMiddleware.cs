using System.Net;
using Common.Enums;
using Common.Exceptions;
using Domain;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace API.Middleware;

public class CustomExceptionHandlerMiddleware
{
    private readonly IWebHostEnvironment _env;
    private readonly IStringLocalizer<CustomExceptionHandlerMiddleware> _localizer;
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next,
        IWebHostEnvironment env,
        ILogger<CustomExceptionHandlerMiddleware> logger,
        IStringLocalizer<CustomExceptionHandlerMiddleware> localizer)
    {
        _next = next;
        _env = env;
        _logger = logger;
        _localizer = localizer;
    }

    public async Task Invoke(HttpContext context)
    {
        string message = null;
        var httpStatusCode = HttpStatusCode.InternalServerError;
        var apiStatusCode = ApiResultStatusCode.ServerError;

        try
        {
            await _next(context);
        }
        catch (AppException exception)
        {
            _logger.LogError(exception, exception.Message);
            httpStatusCode = exception.HttpStatusCode;
            apiStatusCode = exception.ApiStatusCode;

            if (_env.IsDevelopment())
            {
                var dic = new Dictionary<string, string>
                {
                    ["Exception"] = exception.Message,
                    ["StackTrace"] = exception.StackTrace
                };
                if (exception.InnerException != null)
                {
                    dic.Add("InnerException.Exception", exception.InnerException.Message);
                    dic.Add("InnerException.StackTrace", exception.InnerException.StackTrace);
                }

                if (exception.AdditionalData != null)
                {
                    dic.Add("AdditionalData", JsonConvert.SerializeObject(exception.AdditionalData));
                }

                message = JsonConvert.SerializeObject(dic);
            }
            else
            {
                message = exception.Message;
            }

            await WriteToResponseAsync();
        }
        catch (SecurityTokenExpiredException exception)
        {
            _logger.LogError(exception, exception.Message);
            SetUnAuthorizeResponse(exception);
            await WriteToResponseAsync();
        }
        catch (UnauthorizedAccessException exception)
        {
            _logger.LogError(exception, exception.Message);
            SetUnAuthorizeResponse(exception);
            await WriteToResponseAsync();
        }

        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            if (_env.IsDevelopment())
            {
                var dic = new Dictionary<string, string>
                {
                    ["Exception"] = exception.Message,
                    ["StackTrace"] = exception.StackTrace
                };
                message = JsonConvert.SerializeObject(dic);
            }

            await WriteToResponseAsync();
        }

        async Task WriteToResponseAsync()
        {
            if (context.Response.HasStarted)
            {
                throw new InvalidOperationException(
                    "The response has already started, the http status code middleware will not be executed.");
            }

            var result = new ServiceResult(false, apiStatusCode, _localizer[message]);
            var json = JsonConvert.SerializeObject(result);

            context.Response.StatusCode = (int)httpStatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }

        void SetUnAuthorizeResponse(Exception exception)
        {
            httpStatusCode = HttpStatusCode.Unauthorized;
            apiStatusCode = ApiResultStatusCode.UnAuthorized;

            if (_env.IsDevelopment())
            {
                var dic = new Dictionary<string, string>
                {
                    ["Exception"] = exception.Message,
                    ["StackTrace"] = exception.StackTrace
                };
                if (exception is SecurityTokenExpiredException tokenException)
                {
                    dic.Add("Expires", tokenException.Expires.ToString());
                }

                message = JsonConvert.SerializeObject(dic);
            }
        }
    }
}