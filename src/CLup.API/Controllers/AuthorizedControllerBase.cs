using System.Security.Claims;
using CLup.Application.Auth;
using CLup.Domain.Users.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLup.API.Controllers;

[ApiController]
[Authorize(Policy = Policies.User)]
public abstract class AuthorizedControllerBase : ControllerBase
{
    public UserId GetUserIdFromJwt()
    {
        var id = Guid.Parse((ReadOnlySpan<char>)User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        return UserId.Create(id);
    }
};
