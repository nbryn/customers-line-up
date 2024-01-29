using CLup.API.Contracts.Messages.BusinessMarksMessageAsDeleted;
using CLup.API.Contracts.Messages.SendBusinessMessage;
using CLup.API.Contracts.Messages.SendUserMessage;
using CLup.API.Contracts.Messages.UserMarksMessageAsDeleted;
using CLup.Application.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CLup.API.Controllers;

[Route("api/message")]
public class MessageController : AuthorizedControllerBase
{
    private readonly IMediator _mediator;

    public MessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SendUserMessage([FromBody] SendUserMessageRequest request)
    {
        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));

        return this.CreateActionResult(result);
    }

    [HttpPost]
    [Route("business")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SendBusinessMessage([FromBody] SendBusinessMessageRequest request)
    {
        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));

        return this.CreateActionResult(result);
    }

    [HttpPut]
    [Route("user/delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UserMarksMessageAsDeleted([FromBody] UserMarksMessageAsDeletedRequest request)
    {
        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));

        return this.CreateActionResult(result);
    }

    [HttpPut]
    [Route("business/delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> BusinessMarksMessageAsDeleted(
        [FromBody] BusinessMarksMessageAsDeletedRequest request)
    {
        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));

        return this.CreateActionResult(result);
    }
}
