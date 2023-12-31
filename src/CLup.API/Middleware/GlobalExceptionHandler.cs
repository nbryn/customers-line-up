using CLup.Application.Shared;
using Microsoft.AspNetCore.Diagnostics;

namespace CLup.API.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private const string ErrorMessage = "Internal Server Error";
    private readonly RequestDelegate _next;

    public GlobalExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        // TODO: Add logging
        var problemDetails =
            new ProblemDetails(HttpCode.InternalServerError, new Dictionary<string, string>(), ErrorMessage);
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
