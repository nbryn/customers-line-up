using System.Collections.Generic;
using System.Threading.Tasks;
using CLup.Application.Auth;
using CLup.Application.Businesses.Employees.Commands;
using CLup.Application.Businesses.Employees.Queries;
using CLup.Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLup.Application.Businesses.Employees
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [Route("business/{businessId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<EmployeeDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EmployeesByBusiness([FromRoute] FetchEmployeesQuery query)
        {
            var result = await _mediator.Send(query);

            return this.CreateActionResult(result);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewEmployee(CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [HttpDelete]
        [Route("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveEmployee(string email, [FromQuery] string businessId)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand(businessId, email));

            return this.CreateActionResult(result);
        }
    }
}