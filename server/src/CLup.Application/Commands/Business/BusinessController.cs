using System.Security.Claims;
using System.Threading.Tasks;
using CLup.Application.Auth;
using CLup.Application.Commands.Business.Employee.Models;
using CLup.Application.Commands.Business.Models;
using CLup.Application.Commands.Business.TimeSlot.Models;
using CLup.Application.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLup.Application.Commands.Business
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
        public async Task<IActionResult> CreateBusiness([FromBody] CreateBusinessCommand command)
        {
            var ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            command.OwnerEmail = ownerEmail;

            var result = await _mediator.Send(command);

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
        
        [HttpDelete]
        [Route("booking/{businessId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBooking(string businessId, [FromQuery] string bookingId)
        {
            string ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
 
            var response = await _mediator.Send(new DeleteBookingCommand(ownerEmail, bookingId, businessId));

            return this.CreateActionResult(response);
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
    }
}