using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CLup.Features.Auth;
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
        public async Task<IActionResult> Create(string timeSlotId)
        {
            string userMail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var response = await _mediator.Send(new CreateBooking.Command(userMail, timeSlotId));

            return this.CreateActionResult(response);
        }

        [HttpDelete]
        [Route("user/{bookingId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UserDeleteBooking([FromRoute] UserDeleteBooking.Command command)
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
 
            var response = await _mediator.Send(new BusinessDeleteBooking.Command(ownerEmail, bookingId, businessId));

            return this.CreateActionResult(response);
        }

        [HttpGet]
        [Route("user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BookingDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UserBookings()
        {
            string userMail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var response = await _mediator.Send(new UserBookings.Query(userMail));

            return Ok(response);
        }

        [HttpGet]
        [Route("business/{businessId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BookingDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BusinessBookings([FromRoute] BusinessBookings.Query query)
        {
            var response = await _mediator.Send(query);

            return this.CreateActionResult(response);
        }
    }
}