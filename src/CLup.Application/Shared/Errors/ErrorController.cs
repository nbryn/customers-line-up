using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CLup.Application.Shared.Errors
{

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("error")]
    public class ErrorController : ControllerBase
    {
        public ErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;
            var code = 500;
            
            Response.StatusCode = code;

            return new ErrorResponse(exception);
        }
    }
}