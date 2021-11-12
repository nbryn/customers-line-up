using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CLup.Application.Shared.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult ErrorResult(this ControllerBase controller, IEnumerable<IdentityError> errors)
        {
            return controller.BadRequest(errors);
        }

        public static IActionResult CreateActionResult(
            this ControllerBase controller,
            Result result,
            HttpStatusCode successStatusCode = HttpStatusCode.OK)
        {
            return result.Failure ? Error(controller, result) : controller.StatusCode((int)successStatusCode);
        }

        public static IActionResult CreateActionResult<T>(
            this ControllerBase controller,
            Result<T> result,
            HttpStatusCode successStatusCode = HttpStatusCode.OK)
        {
            return result.Failure ? Error(controller, result) : controller.StatusCode((int)successStatusCode, result.Value);
        }

        private static IActionResult Error(
        this ControllerBase controller,
        Result result)
        {
            return result.Code switch
            {
                HttpCode.NotFound => controller.NotFound(result.Error ?? null),
                HttpCode.Forbidden => controller.Forbid(result.Error ?? null),
                HttpCode.Conflict => controller.Conflict(result.Error ?? null),
                HttpCode.Unauthorized => controller.Unauthorized(result.Error ?? null),
                _ => controller.BadRequest(result.Error ?? null)
            };
        }
    }
}