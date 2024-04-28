using CLup.API.Auth;
using CLup.API.Extensions;
using CLup.API.Users.Contracts.UpdateUser;
using Microsoft.AspNetCore.Mvc;
using ProblemDetails = CLup.Application.Shared.ProblemDetails;

namespace CLup.API.Users;

[Route("api/user")]
public sealed class UserController : AuthorizedControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("update")]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((typeof(ProblemDetails)), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
    {
        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));

        return this.CreateActionResult(result);
    }
}
