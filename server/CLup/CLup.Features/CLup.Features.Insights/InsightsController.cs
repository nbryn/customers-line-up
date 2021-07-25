using System.Security.Claims;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CLup.Features.Auth;
using CLup.Features.Extensions;

namespace CLup.Features.Insights
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("[controller]")]
    public class InsightsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InsightsController(IMediator mediator) => _mediator = mediator;

        [Authorize(Policy = Policies.User)]
        [Route("user/booking")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserBookingInsights.Model))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchUserBookingInsights()
        {
            string userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(new UserBookingInsights.Query(userEmail));

            return this.CreateActionResult(result);
        }

        [Authorize(Policy = Policies.User)]
        [Route("user/business")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserBusinessInsights.Model))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchUserBusinessInsights()
        {
            string userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(new UserBusinessInsights.Query(userEmail));

            return this.CreateActionResult(result);
        }
    }
}
