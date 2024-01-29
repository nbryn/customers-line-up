using CLup.API.Contracts.Users.UpdateUser;
using CLup.Application.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CLup.API.Controllers;

[Route("api/user")]
public class UserController : AuthorizedControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("update")]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
    {
        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));

        return this.CreateActionResult(result);
    }
}
