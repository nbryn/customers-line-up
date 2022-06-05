using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace CLup.Application.Shared.Extensions
{
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
                HttpCode.NotFound => controller.NotFound(result.Error),
                HttpCode.Forbidden => controller.Forbid(result.Error),
                HttpCode.Conflict => controller.Conflict(result.Error),
                HttpCode.Unauthorized => controller.Unauthorized(result.Error),
                _ => controller.BadRequest(result.Error)
            };
    }
}