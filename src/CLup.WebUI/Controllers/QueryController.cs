using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CLup.Application.Auth;
using CLup.Application.Businesses.Queries;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Models;
using CLup.Application.Users.Queries;
using CLup.Application.Users.Queries.NotEmployed;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLup.WebUI.Controllers
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("api/query")]
    public class QueryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public QueryController(IMediator mediator) => _mediator = mediator;


        [HttpGet]
        [Route("user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchUserAggregate()
        {
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(new UserAggregateQuery(userEmail));

            return this.CreateActionResult(result);
        }

        [HttpGet]
        [Route("business/all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BusinessDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllBusinesses()
        {
            var result = await _mediator.Send(new AllBusinessesQuery());

            return this.CreateActionResult(result);
        }

        [HttpGet]
        [Route("business/types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> FetchBusinessTypes()
        {
            return Ok(await _mediator.Send(new BusinessTypesQuery()));
        }

        [Route("business/insights")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserBusinessInsightsQuery.Model))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchBusinessInsights()
        {
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(new UserBusinessInsightsQuery.Query(userEmail));

            return this.CreateActionResult(result);
        }
        
        [Route("user/notEmployedByBusiness/{businessId}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersNotEmployedByBusinessResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllUsersNotAlreadyEmployedByBusiness([FromRoute] UsersNotEmployedByBusinessQuery query)
        {
            var result = await _mediator.Send(query);

            return this.CreateActionResult(result);
        }

        [Route("user/insights")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserBookingInsightsQuery.Model))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchUserInsights()
        {
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(new UserBookingInsightsQuery.Query(userEmail));

            return this.CreateActionResult(result);
        }
    }
}