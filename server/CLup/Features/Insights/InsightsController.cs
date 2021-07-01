using System.Security.Claims;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CLup.Auth;
using CLup.Extensions;

namespace CLup.Insights
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("[controller]")]
    public class InsightsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InsightsController(IMediator mediator) => _mediator = mediator;

        [Authorize(Policy = Policies.User)]
        [Route("user")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserInsights.Model))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchUserInsights()
        {
            string userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(new UserInsights.Query(userEmail));

            return this.CreateActionResult(result);
        }
    }
}
