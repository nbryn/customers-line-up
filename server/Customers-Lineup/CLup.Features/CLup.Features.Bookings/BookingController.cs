using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CLup.Features.Auth;
using CLup.Features.Bookings.Commands;
using CLup.Features.Bookings.Queries;
using CLup.Features.Extensions;

namespace CLup.Features.Bookings
{
    [Authorize(Policy = Policies.User)]
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookingController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [Route("{timeSlotId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create(string timeSlotId, [FromQuery] string userId, [FromQuery] string businessId)
        {
            var response = await _mediator.Send(new CreateBookingCommand(userId, timeSlotId, businessId));

            return this.CreateActionResult(response);
        }

        [HttpDelete]
        [Route("user/{bookingId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UserDeleteBooking([FromRoute] UserDeleteBookingCommand command)
        {
            var response = await _mediator.Send(command);

            return this.CreateActionResult(response);
        }

        [HttpDelete]
        [Route("business/{businessId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BusinessDeleteBooking(string businessId, [FromQuery] string bookingId)
        {
            string ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
 
            var response = await _mediator.Send(new BusinessDeleteBookingCommand(ownerEmail, bookingId, businessId));

            return this.CreateActionResult(response);
        }

        [HttpGet]
        [Route("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BookingDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UserBookings([FromRoute] UserBookingsQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet]
        [Route("business/{businessId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BookingDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BusinessBookings([FromRoute] BusinessBookingsQuery query)
        {
            var response = await _mediator.Send(query);

            return this.CreateActionResult(response);
        }
    }
}