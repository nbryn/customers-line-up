using CLup.Application.Bookings.Commands.BusinessDeleteBooking;
using CLup.Application.Bookings.Commands.CreateBooking;
using CLup.Application.Bookings.Commands.UserDeleteBooking;
using CLup.Application.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CLup.API.Controllers;

[Route("api/booking")]
public class BookingController : BaseController
{
    private readonly IMediator _mediator;

    public BookingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("{timeSlotId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateBooking(Guid timeSlotId, [FromQuery] Guid businessId)
    {
        var userId = GetUserIdFromJwt();
        var response = await _mediator.Send(new CreateBookingCommand(userId, timeSlotId, businessId));

        return this.CreateActionResult(response);
    }

    [HttpDelete]
    [Route("user/{bookingId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUserBooking(Guid bookingId)
    {
        var userId = GetUserIdFromJwt();
        var response = await _mediator.Send(new UserDeleteBookingCommand(userId, bookingId));

        return this.CreateActionResult(response);
    }

    [HttpDelete]
    [Route("business/{businessId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBusinessBooking(Guid businessId, [FromQuery] Guid bookingId)
    {
        var ownerId = GetUserIdFromJwt();
        var response = await _mediator.Send(new BusinessDeleteBookingCommand(ownerId, bookingId, businessId));

        return this.CreateActionResult(response);
    }
}
