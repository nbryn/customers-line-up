using CLup.API.Contracts.TimeSlots.DeleteTimeSlot;
using CLup.API.Contracts.TimeSlots.GenerateTimeSlots;
using CLup.API.Extensions;
using CLup.Application.Shared;
using Microsoft.AspNetCore.Mvc;

using ProblemDetails = CLup.Application.Shared.ProblemDetails;

namespace CLup.API.Controllers;

[Route("api/timeSlot")]
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
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GenerateTimeSlots([FromBody] GenerateTimeSlotsRequest request)
    {
        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));
        return this.CreateActionResult(result);
    }

    [HttpDelete]
    [Route("{timeSlotId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTimeSlot([FromRoute] Guid timeSlotId, [FromQuery] Guid businessId)
    {
        var request = new DeleteTimeSlotRequest(timeSlotId, businessId);
        var validationResult = Result.Validate<DeleteTimeSlotRequest, DeleteTimeSlotRequestValidator>(request);
        if (validationResult.Failure)
        {
            return BadRequest(validationResult);
        }

        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));
        return this.CreateActionResult(result);
    }
}
