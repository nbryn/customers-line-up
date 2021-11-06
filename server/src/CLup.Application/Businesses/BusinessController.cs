using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CLup.Application.Auth;
using CLup.Application.Businesses.Commands;
using CLup.Application.Businesses.Employees;
using CLup.Application.Businesses.Employees.Commands;
using CLup.Application.Businesses.Employees.Queries;
using CLup.Application.Businesses.Queries;
using CLup.Application.Businesses.TimeSlots;
using CLup.Application.Businesses.TimeSlots.Commands;
using CLup.Application.Businesses.TimeSlots.Queries;
using CLup.Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLup.Application.Businesses
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("[controller]")]
    public class BusinessController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BusinessController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> NewBusiness([FromBody] CreateBusinessCommand command)
        {

            var ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            command.OwnerEmail = ownerEmail;

            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [HttpGet]
        [Route("owner")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BusinessDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BusinessesByOwner()
        {
            var ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _mediator.Send(new BusinessesByOwnerQuery(ownerEmail));

            return this.CreateActionResult(result);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBusinessData([FromBody] UpdateBusinessCommand command)
        {
            command.OwnerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BusinessDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAll()
        {
            var result = await _mediator.Send(new AllBusinessesQuery());

            return this.CreateActionResult(result);
        }

        [HttpGet]
        [Route("types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> FetchBusinessTypes()
        {
            return Ok(await _mediator.Send(new BusinessTypesQuery()));
        }
        
        [Route("insights")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserBusinessInsightsQuery.Model))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchBusinessInsights()
        {
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(new UserBusinessInsightsQuery.Query(userEmail));

            return this.CreateActionResult(result);
        }
        
        [HttpGet]
        [Route("employee/{businessId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<EmployeeDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchEmployees([FromRoute] FetchEmployeesQuery query)
        {
            var result = await _mediator.Send(query);

            return this.CreateActionResult(result);
        }

        [HttpPost]
        [Route("employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [HttpDelete]
        [Route("employee/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveEmployee(string email, [FromQuery] string businessId)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand(businessId, email));

            return this.CreateActionResult(result);
        }
        
        [HttpPost]
        [Route("timeslot/generate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GenerateTimeSlots([FromBody] GenerateTimeSlotsCommand command)
        {
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [HttpDelete]
        [Route("timeslot/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTimeSlot([FromRoute] DeleteTimeSlotCommand command)
        {
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [HttpGet]
        [Route("timeslot/{businessId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<TimeSlotDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchTimeSlots([FromRoute] TimeSlotsByBusinessQuery query)
        {
            var result = await _mediator.Send(query);

            return this.CreateActionResult(result);
        }

        [HttpGet]
        [Route("timeslot/available")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<TimeSlotDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAvailableTimeSlots([FromQuery] AvailableTimeSlotsByBusinessQuery query)
        {
            var result = await _mediator.Send(query);

            return this.CreateActionResult(result);
        }
    }
}