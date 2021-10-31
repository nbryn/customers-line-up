using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

using CLup.Features.Shared;

namespace CLup.Features.Extensions
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
            if (result.Failure)
            {
                return Error(controller, result);
            }

            return controller.StatusCode((int)successStatusCode);
        }

        public static IActionResult CreateActionResult<T>(
            this ControllerBase controller,
            Result<T> result,
            HttpStatusCode successStatusCode = HttpStatusCode.OK)
        {
            if (result.Failure)
            {
                return Error(controller, result);
            }

            return controller.StatusCode((int)successStatusCode, result.Value);
        }

        private static IActionResult Error(
        this ControllerBase controller,
        Result result)
        {
            if (result.Code == HttpCode.NotFound)
            {
                return controller.NotFound(result.Error ?? null);
            }
            if (result.Code == HttpCode.Forbidden)
            {
                return controller.Forbid(result.Error ?? null);
            }

            if (result.Code == HttpCode.Conflict)
            {
                return controller.Conflict(result.Error ?? null);
            }

            if (result.Code == HttpCode.Unauthorized)
            {
                return controller.Unauthorized(result.Error ?? null);
            }

            return controller.BadRequest(result.Error ?? null);

        }
    }
}