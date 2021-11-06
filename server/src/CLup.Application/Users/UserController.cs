using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CLup.Application.Auth;
using CLup.Application.Extensions;
using CLup.Application.Users.Commands;
using CLup.Application.Users.Queries;
using CLup.Application.Users.Queries.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLup.Application.Users
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator) => _mediator = mediator;
        
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
        
        [Route("update")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserInfoCommand command)
        {
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }
        
        [Route("{userId}/messages")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FetchMessagesResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMessages([FromRoute] FetchMessagesQuery query)
        {
            query.UserEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(query);

            return this.CreateActionResult(result);
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