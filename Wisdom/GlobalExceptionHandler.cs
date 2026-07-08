using Microsoft.AspNetCore.Diagnostics;

namespace Wisdom;

public class GlobalExceptionHandler : IExceptionHandler
{
    // Attributes
    private readonly ILogger<GlobalExceptionHandler> _logger;

    // Constructor
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) => _logger = logger;

    // handler
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Unhandled exception occurred: {Message}", exception.Message);

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = "application/json";

        var response = new
        {
            status = 500,
            message = "An internal server error occurred. Please try again later."
        };

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true; // indicates the exception was handled
    }
}