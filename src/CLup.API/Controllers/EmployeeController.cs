using CLup.API.Contracts.Employees.CreateEmployee;
using CLup.API.Contracts.Employees.DeleteEmployee;
using CLup.API.Extensions;
using CLup.Application.Shared;
using Microsoft.AspNetCore.Mvc;

using ProblemDetails = CLup.Application.Shared.ProblemDetails;

namespace CLup.API.Controllers;

[Route("api/employee")]
public sealed class EmployeeController : AuthorizedControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequest request)
    {
        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));
        return this.CreateActionResult(result);
    }

    [HttpDelete]
    [Route("{employeeId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEmployee([FromRoute] Guid employeeId, [FromQuery] Guid businessId)
    {
        var request = new DeleteEmployeeRequest(employeeId, businessId);
        var validationResult = Result.Validate<DeleteEmployeeRequest, DeleteEmployeeRequestValidator>(request);
        if (validationResult.Failure)
        {
            return BadRequest(validationResult);
        }

        var result = await _mediator.Send(request.MapToCommand(GetUserIdFromJwt()));
        return this.CreateActionResult(result);
    }
}
