using CLup.Application.Shared.Extensions;
using CLup.Application.TimeSlots.Commands.DeleteTimeSlot;
using CLup.Application.TimeSlots.Commands.GenerateTimeSlot;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CLup.API.Controllers;

[Microsoft.AspNetCore.Components.Route("api/timeslot")]
public class TimeSlotController : BaseController
{
    private readonly IMediator _mediator;

    public TimeSlotController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> GenerateTimeSlots([FromBody] GenerateTimeSlotsCommand command)
    {
        command.OwnerId = GetUserIdFromJwt();
        var result = await _mediator.Send(command);

        return this.CreateActionResult(result);
    }

    [HttpDelete]
    [Route("{timeSlotId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTimeSlot(Guid timeSlotId, [FromQuery] Guid businessId)
    {
        var ownerId = GetUserIdFromJwt();
        var result = await _mediator.Send(new DeleteTimeSlotCommand(ownerId, businessId, timeSlotId));

        return this.CreateActionResult(result);
    }
}
