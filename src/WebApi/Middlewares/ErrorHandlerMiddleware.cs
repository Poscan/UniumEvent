using System.Net.Mime;
using System.Text.Json;
using Application.Exceptions;
using Application.Wrappers;
using Domain.Common.Exceptions;
using WebApi.Exceptions;

namespace WebApi.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            var response = context.Response;
            response.StatusCode = StatusCodes.Status200OK;
            response.ContentType = MediaTypeNames.Application.Json;

            IEnumerable<ResponseError>? errors;
            switch (exception)
            {
                case ApiException e:
                    errors = ApiExceptionHandler(e);
                    break;
                case EntityNotFoundException e:
                    errors = EntityNotFoundExceptionHandler(e);
                    break;
                case ValidationException e:
                    errors = ValidationExceptionHandler(e);
                    break;
                case UnauthorizedAccessException e:
                    response.StatusCode = 401;
                    errors = UnauthorizedAccessExceptionHandler(e);
                    break;
                default:
                    errors = UnknownExceptionHandler(exception);
                    break;
            }

            var failResponse = Response.Fail<string>(errors);
            var serializedResponse = JsonSerializer.Serialize(failResponse);
            
            await response.WriteAsync(serializedResponse);
        }
    }

    private IEnumerable<ResponseError> ApiExceptionHandler(ApiException exception)
    {
        yield return ResponseError.New(exception.Message);
    }

    private IEnumerable<ResponseError> EntityNotFoundExceptionHandler(EntityNotFoundException exception)
    {
        yield return ResponseError.New(exception.Message);
    }

    private IEnumerable<ResponseError> ValidationExceptionHandler(ValidationException exception)
    {
        return exception.Errors.Select(x => ResponseError.New(x));
    }
    
    private IEnumerable<ResponseError> UnauthorizedAccessExceptionHandler(UnauthorizedAccessException unauthorizedAccessException)
    {
        yield return ResponseErrors.Common.Unauthorized;
    }

    private IEnumerable<ResponseError> UnknownExceptionHandler(Exception exception)
    {
        const string unknownErrorMessage = "Unknown error, code: {0}";
        var errorGuid = Guid.NewGuid();
        _logger.LogError(exception, unknownErrorMessage, errorGuid);
        
        yield return ResponseErrors.Common.InternalServerError(errorGuid);
    }
}