using CLup.API.Contracts.TimeSlots.DeleteTimeSlot;
using CLup.API.Contracts.TimeSlots.GenerateTimeSlots;
using CLup.API.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CLup.API.Controllers;

[Route("api/timeslot")]
public sealed class TimeSlotController : AuthorizedControllerBase
{
    private readonly IMediator _mediator;

    public TimeSlotController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GenerateTimeSlots([FromBody] GenerateTimeSlotsRequest request)
    {
        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));

        return this.CreateActionResult(result);
    }

    [HttpDelete]
    [Route("{timeSlotId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTimeSlot([FromRoute] Guid timeSlotId, [FromQuery] Guid businessId)
    {
        var request = new DeleteTimeSlotRequest(timeSlotId, businessId);
        // TODO: Move this to mediator?
        var validationResult = new DeleteTimeSlotRequestValidator().Validate(request);
        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));

        return this.CreateActionResult(result);
    }
}
