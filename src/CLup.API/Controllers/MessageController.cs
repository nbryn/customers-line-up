using CLup.Application.Messages.Commands.MarkMessageAsDeleted;
using CLup.Application.Messages.Commands.SendMessage;
using CLup.Application.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CLup.API.Controllers;

[Route("api/message")]
public class MessageController : BaseController
{
    private readonly IMediator _mediator;

    public MessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("message/send")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageCommand command)
    {
        var response = await _mediator.Send(command);

        return this.CreateActionResult(response);
    }

    [HttpPut]
    [Route("message/delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> MarkMessageAsDeleted([FromBody] MarkMessageAsDeletedCommand command)
    {
        command.UserId = GetUserIdFromJwt();
        var response = await _mediator.Send(command);

        return this.CreateActionResult(response);
    }
}
