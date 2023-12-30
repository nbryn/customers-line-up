using CLup.Application.Employees.Commands.CreateEmployee;
using CLup.Application.Employees.Commands.DeleteEmployee;
using CLup.Application.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CLup.API.Controllers;

[Route("api/employee")]
public class EmployeeController : BaseController
{
    private readonly IMediator _mediator;

    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand command)
    {
        command.OwnerId = GetUserIdFromJwt();
        var result = await _mediator.Send(command);

        return this.CreateActionResult(result);
    }

    [HttpDelete]
    [Route("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveEmployee(Guid userId, [FromQuery] Guid businessId)
    {
        var ownerId = GetUserIdFromJwt();
        var result = await _mediator.Send(new DeleteEmployeeCommand(ownerId, businessId, userId));

        return this.CreateActionResult(result);
    }
}
