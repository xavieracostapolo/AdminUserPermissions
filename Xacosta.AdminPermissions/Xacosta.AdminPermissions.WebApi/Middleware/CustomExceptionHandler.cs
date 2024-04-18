using Microsoft.AspNetCore.Diagnostics;
using Xacosta.AdminPermissions.Application.Exceptions;

namespace Xacosta.AdminPermissions.WebApi.Middleware
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<CustomExceptionHandler> _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, exception.Message);
            httpContext.Response.StatusCode = exception switch
            {
                FluentValidation.ValidationException => StatusCodes.Status400BadRequest,
                //PrestamoExisteException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            var msg = new
            {
                statusCode = httpContext.Response.StatusCode,
                type = exception.GetType().Name,
                title = "An unexpected error occurred",
                detail = exception.Message,
                instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
            };

            await httpContext.Response.WriteAsJsonAsync(msg, cancellationToken: cancellationToken);

            return true;
        }
    }
}
