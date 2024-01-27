using System.Security.Claims;
using CLup.Application.Auth;
using CLup.Application.Auth.Commands.Login;
using CLup.Application.Auth.Commands.Register;
using CLup.Application.Shared.Extensions;
using CLup.Domain.Users.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLup.API.Controllers;

[ApiController]
[Route("api/")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [Route("register")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await _mediator.Send(command);

        return this.CreateActionResult(result);
    }

    [AllowAnonymous]
    [Route("login")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        command.Id = UserId.Create(Guid.Parse((ReadOnlySpan<char>)User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
        var result = await _mediator.Send(command);

        return this.CreateActionResult(result);
    }
}
