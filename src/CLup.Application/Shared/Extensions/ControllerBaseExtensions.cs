using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace CLup.Application.Shared.Extensions;

using Result = Result;

public static class ControllerBaseExtensions
{
    public static IActionResult CreateActionResult(
        this ControllerBase controller,
        Result result,
        HttpStatusCode successStatusCode = HttpStatusCode.OK)
        => result.Failure ? Error(controller, result) : controller.StatusCode((int)successStatusCode);

    public static IActionResult CreateActionResult<T>(
        this ControllerBase controller,
        Result<T> result,
        HttpStatusCode successStatusCode = HttpStatusCode.OK)
        => result.Failure ? Error(controller, result) : controller.StatusCode((int)successStatusCode, result.Value);

    private static IActionResult Error(
        this ControllerBase controller,
        Result result)
        => result.Code switch
        {
            HttpCode.NotFound => controller.NotFound(result.ToProblemDetails()),
            HttpCode.Forbidden => controller.Forbid(),
            HttpCode.Conflict => controller.Conflict(result.ToProblemDetails()),
            HttpCode.Unauthorized => controller.Unauthorized(result.ToProblemDetails()),
            _ => controller.BadRequest(result.ToProblemDetails())
        };
}
