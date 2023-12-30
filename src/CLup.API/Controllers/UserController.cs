using CLup.Application.Shared.Extensions;
using CLup.Application.Users.Commands.UpdateUserInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CLup.API.Controllers;

[Route("api")]
public class UserController : BaseController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("user/update")]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
    {
        command.UserId = GetUserIdFromJwt();
        var result = await _mediator.Send(command);

        return this.CreateActionResult(result);
    }
}
