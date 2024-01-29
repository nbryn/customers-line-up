using CLup.API.Contracts.Bookings.CreateBooking;
using CLup.API.Contracts.Bookings.DeleteBusinessBooking;
using CLup.API.Contracts.Bookings.DeleteUserBooking;
using CLup.Application.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CLup.API.Controllers;

[Route("api/booking")]
public class BookingController : AuthorizedControllerBase
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
    public async Task<IActionResult> CreateBooking(CreateBookingRequest request)
    {
        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));

        return this.CreateActionResult(result);
    }

    [HttpDelete]
    [Route("user/{bookingId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUserBooking([FromRoute] DeleteUserBookingRequest request)
    {
        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));

        return this.CreateActionResult(result);
    }

    [HttpDelete]
    [Route("business/{businessId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBusinessBooking(DeleteBusinessBookingRequest request)
    {
        var response = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));

        return this.CreateActionResult(response);
    }
}
