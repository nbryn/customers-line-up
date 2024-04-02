using CLup.API.Contracts.Messages.MarkMessageAsDeletedForBusiness;
using CLup.API.Contracts.Messages.MarkMessageAsDeletedForUser;
using CLup.API.Contracts.Messages.SendBusinessMessage;
using CLup.API.Contracts.Messages.SendUserMessage;
using CLup.API.Extensions;
using Microsoft.AspNetCore.Mvc;

using ProblemDetails = CLup.Application.Shared.ProblemDetails;

namespace CLup.API.Controllers;

[Route("api/message")]
public sealed class MessageController : AuthorizedControllerBase
{
    private readonly IMediator _mediator;

    public MessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((typeof(ProblemDetails)), StatusCodes.Status400BadRequest)]
    [ProducesResponseType((typeof(ProblemDetails)), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SendUserMessage([FromBody] SendUserMessageRequest request)
    {
        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));

        return this.CreateActionResult(result);
    }

    [HttpPost]
    [Route("business")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((typeof(ProblemDetails)), StatusCodes.Status400BadRequest)]
    [ProducesResponseType((typeof(ProblemDetails)), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SendBusinessMessage([FromBody] SendBusinessMessageRequest request)
    {
        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));

        return this.CreateActionResult(result);
    }

    [HttpPatch]
    [Route("user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((typeof(ProblemDetails)), StatusCodes.Status400BadRequest)]
    [ProducesResponseType((typeof(ProblemDetails)), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> MarkMessageAsDeletedForUser([FromBody] MarkMessageAsDeletedForUserRequest request)
    {
        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));

        return this.CreateActionResult(result);
    }

    [HttpPatch]
    [Route("business")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((typeof(ProblemDetails)), StatusCodes.Status400BadRequest)]
    [ProducesResponseType((typeof(ProblemDetails)), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> MarkMessageAsDeletedForBusiness(
        [FromBody] MarkMessageAsDeletedForBusinessRequest request)
    {
        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));

        return this.CreateActionResult(result);
    }
}
