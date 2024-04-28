using CLup.API.Auth;
using CLup.API.Bookings.Contracts.CreateBooking;
using CLup.API.Bookings.Contracts.DeleteBusinessBooking;
using CLup.API.Bookings.Contracts.DeleteUserBooking;
using CLup.API.Extensions;
using CLup.Application.Shared;
using Microsoft.AspNetCore.Mvc;
using ProblemDetails = CLup.Application.Shared.ProblemDetails;

namespace CLup.API.Bookings;

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
        var validationResult = Result.Validate<DeleteUserBookingRequest, DeleteUserBookingRequestValidator>(request);
        if (validationResult.Failure)
        {
            return BadRequest(validationResult.ToProblemDetails());
        }

        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));
        return this.CreateActionResult(result);
    }

    [HttpDelete]
    [Route("business/{businessId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((typeof(ProblemDetails)), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBusinessBooking([FromRoute] Guid businessId, [FromQuery] Guid bookingId)
    {
        var request = new DeleteBusinessBookingRequest(businessId, bookingId);
        var validationResult = Result.Validate<DeleteBusinessBookingRequest, DeleteBusinessBookingRequestValidator>(request);
        if (validationResult.Failure)
        {
            return BadRequest(validationResult.ToProblemDetails());
        }

        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));
        return this.CreateActionResult(result);
    }
}
