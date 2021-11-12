using System.Threading.Tasks;
using CLup.Application.Auth;
using CLup.Application.Commands.User.Models;
using CLup.Application.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLup.WebUI.Controllers
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("user")]
    public class UserCommandController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserCommandController(IMediator mediator) => _mediator = mediator;
        
        [Route("update")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserInfoCommand command)
        {
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }
        
        [HttpPost]
        [Route("booking/{timeSlotId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateBooking(string timeSlotId, [FromQuery] string userId, [FromQuery] string businessId)
        {
            var response = await _mediator.Send(new CreateBookingCommand(userId, timeSlotId, businessId));

            return this.CreateActionResult(response);
        }

        [HttpDelete]
        [Route("booking/{bookingId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBooking([FromRoute] DeleteBookingCommand command)
        {
            var response = await _mediator.Send(command);

            return this.CreateActionResult(response);
        }
    }
}