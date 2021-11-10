using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CLup.Application.Shared.Errors
{

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
        public ErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;
            var code = 500;

            if (exception is BookingExistsException) code = 409;

            Response.StatusCode = code;

            return new ErrorResponse(exception);
        }
    }
}