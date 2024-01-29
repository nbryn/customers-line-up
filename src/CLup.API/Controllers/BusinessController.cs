using CLup.API.Contracts.Businesses.CreateBusiness;
using CLup.API.Contracts.Businesses.UpdateBusiness;
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
    public async Task<IActionResult> CreateBusiness([FromBody] CreateBusinessRequest request)
    {
        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));

        return this.CreateActionResult(result);
    }

    [HttpPut]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBusiness([FromBody] UpdateBusinessRequest request)
    {
        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));

        return this.CreateActionResult(result);
    }
}
