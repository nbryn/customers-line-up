using CLup.API.Contracts.Bookings.CreateBooking;
using CLup.API.Contracts.Bookings.DeleteBusinessBooking;
using CLup.API.Contracts.Bookings.DeleteUserBooking;
using CLup.API.Extensions;
using Microsoft.AspNetCore.Mvc;

using ProblemDetails = CLup.Application.Shared.ProblemDetails;

namespace CLup.API.Controllers;

[Route("api/booking")]
public sealed class BookingController : AuthorizedControllerBase
{
    private readonly IMediator _mediator;

    public BookingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((typeof(ProblemDetails)), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
    {
        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));

        return this.CreateActionResult(result);
    }

    [HttpDelete]
    [Route("user/{bookingId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((typeof(ProblemDetails)), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUserBooking([FromRoute] Guid bookingId)
    {
        var request = new DeleteUserBookingRequest(bookingId);
        return await ValidateAndContinueOnSuccess<DeleteUserBookingRequest, DeleteUserBookingRequestValidator>(
            request,
            async () => await _mediator.Send(request.MapToCommand(GetUserIdFromJwt())));
    }

    [HttpDelete]
    [Route("business/{businessId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((typeof(ProblemDetails)), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBusinessBooking([FromRoute] Guid businessId, [FromQuery] Guid bookingId)
    {
        var request = new DeleteBusinessBookingRequest(businessId, bookingId);
        return await ValidateAndContinueOnSuccess<DeleteBusinessBookingRequest, DeleteBusinessBookingRequestValidator>(
            request,
            async () => await _mediator.Send(request.MapToCommand(GetUserIdFromJwt())));
    }
}
