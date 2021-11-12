using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CLup.Application.Auth;
using CLup.Application.Queries.User.Booking;
using CLup.Application.Queries.User.General;
using CLup.Application.Queries.User.Message;
using CLup.Application.Queries.User.NotEmployed;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLup.Application.Queries.User
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("query/user")]
    public class UserQueryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserQueryController(IMediator mediator) => _mediator = mediator;
        
        [Route("")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchUserInfo()
        {
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _mediator.Send(new UserInfoQuery(userEmail));

            return this.CreateActionResult(result);
        }
        
        [Route("all/{businessId}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersNotEmployedByBusinessResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllUsersNotAlreadyEmployedByBusiness([FromRoute] UsersNotEmployedByBusinessQuery query)
        {
            var result = await _mediator.Send(query);

            return this.CreateActionResult(result);
        }
        
        [Route("all")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<UserDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllUsers()
        {
            var result = await _mediator.Send(new FetchAllUsersQuery());

            return this.CreateActionResult(result);
        }
        
        [Route("{userId}/messages")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FetchMessagesResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchMessages([FromRoute] FetchMessagesQuery query)
        {
            query.UserEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(query);

            return this.CreateActionResult(result);
        }
        
        [HttpGet]
        [Route("booking/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BookingDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchBookings([FromRoute] FetchBookingsQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }
        
        [Route("insights")]
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