using CLup.Application.Businesses.Commands.CreateBusiness;
using CLup.Application.Businesses.Commands.UpdateBusiness;
using CLup.Application.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CLup.API.Controllers;

[Route("api/business")]
public class BusinessController : AuthorizedControllerBase
{
    private readonly IMediator _mediator;

    public BusinessController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateBusiness([FromBody] CreateBusinessCommand command)
    {
        command.OwnerId = GetUserIdFromJwt();
        var result = await _mediator.Send(command);

        return this.CreateActionResult(result);
    }

    [HttpPut]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBusiness([FromBody] UpdateBusinessCommand command)
    {
        command.OwnerId = GetUserIdFromJwt();
        var result = await _mediator.Send(command);

        return this.CreateActionResult(result);
    }
}
