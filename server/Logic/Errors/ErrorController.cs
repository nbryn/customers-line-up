using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Logic.Errors
{

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
        public ErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;
            var code = 500; // Internal Server Error by default

            if (exception is BookingExistsException) code = 409; // Not Found

            Response.StatusCode = code; // You can use HttpStatusCode enum instead

            return new ErrorResponse(exception); // Your error model
        }
    }
}