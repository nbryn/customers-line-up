using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

using CLup.Util;

namespace CLup.Extensions
{

    public static class ControllerBaseExtensions
    {

        public static IActionResult ErrorResult(this ControllerBase controller, string code, string description)
        {
            return controller.BadRequest(new ErrorResponse[]
            {
                new ErrorResponse(code, description)
            });
        }

        public static IActionResult ErrorResult(this ControllerBase controller, IEnumerable<IdentityError> errors)
        {
            return controller.BadRequest(errors);
        }

        public static IActionResult CreateActionResult<T>(
            this ControllerBase controller,
            ServiceResponse<T> response,
            HttpStatusCode successStatusCode = HttpStatusCode.OK,
            bool includeResponseData = true)
        {

            if (response.HasErrors)
            {
                return Error(controller, response);
            }


            if (includeResponseData)
            {
                return controller.StatusCode((int)successStatusCode, response._response);
            }

            return controller.StatusCode((int)successStatusCode);
        }

        public static IActionResult CreateActionResult(
            this ControllerBase controller,
            ServiceResponse response,
            HttpStatusCode successStatusCode = HttpStatusCode.OK)
        {
            if (response.HasErrors)
            {
                return Error(controller, response);
            }

            return controller.StatusCode((int)successStatusCode);
        }


        private static IActionResult Error(
            this ControllerBase controller,
            ServiceResponse response)
        {
            if (response._statusCode == HttpCode.NotFound)
            {
                return controller.NotFound(response._message ?? null);
            }
            if (response._statusCode == HttpCode.Forbidden)
            {
                return controller.Forbid(response._message ?? null);
            }

            if (response._statusCode == HttpCode.Conflict)
            {
                return controller.Conflict(response._message ?? null);
            }

            if (response._statusCode == HttpCode.Unauthorized)
            {
                return controller.Unauthorized(response._message ?? null);
            }

            return controller.BadRequest(response._message ?? null);

        }
    }
}