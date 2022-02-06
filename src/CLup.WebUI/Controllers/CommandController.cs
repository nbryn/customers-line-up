using System.Security.Claims;
using System.Threading.Tasks;
using CLup.Application.Auth;
using CLup.Application.Commands.Business.Create;
using CLup.Application.Commands.Business.DeleteBooking;
using CLup.Application.Commands.Business.Employee.Create;
using CLup.Application.Commands.Business.Employee.Delete;
using CLup.Application.Commands.Business.TimeSlot.Delete;
using CLup.Application.Commands.Business.TimeSlot.Generate;
using CLup.Application.Commands.Business.Update;
using CLup.Application.Commands.Shared.Message.MarkAsDeleted;
using CLup.Application.Commands.Shared.Message.Send;
using CLup.Application.Commands.User.CreateBooking;
using CLup.Application.Commands.User.DeleteBooking;
using CLup.Application.Commands.User.Update;
using CLup.Application.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLup.WebUI.Controllers
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("api")]
    public class CommandController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CommandController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [Route("business")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateBusiness([FromBody] CreateBusinessCommand command)
        {
            command.OwnerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [HttpPut]
        [Route("business/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBusinessData([FromBody] UpdateBusinessCommand command)
        {
            command.OwnerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [HttpDelete]
        [Route("business/{businessId}/booking")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBusinessBooking(string businessId, [FromQuery] string bookingId)
        {
            string ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var response = await _mediator.Send(new BusinessDeleteBookingCommand(ownerEmail, bookingId, businessId));

            return this.CreateActionResult(response);
        }

        [HttpPost]
        [Route("business/employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [HttpDelete]
        [Route("business/employee/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveEmployee(string email, [FromQuery] string businessId)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand(businessId, email));

            return this.CreateActionResult(result);
        }

        [HttpPost]
        [Route("business/timeslot/generate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GenerateTimeSlots([FromBody] GenerateTimeSlotsCommand command)
        {
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [HttpDelete]
        [Route("business/timeslot/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTimeSlot([FromRoute] DeleteTimeSlotCommand command)
        {
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [Route("user/update")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserInfoCommand command)
        {
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [HttpPost]
        [Route("user/booking/{timeSlotId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateBooking(string timeSlotId, [FromQuery] string userId, [FromQuery] string businessId)
        {
            var response = await _mediator.Send(new CreateBookingCommand(userId, timeSlotId, businessId));

            return this.CreateActionResult(response);
        }

        [HttpDelete]
        [Route("user/booking/{bookingId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserBooking([FromRoute] UserDeleteBookingCommand command)
        {
            var response = await _mediator.Send(command);

            return this.CreateActionResult(response);
        }

        [HttpPost]
        [Route("message/send")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageCommand command)
        {
            var response = await _mediator.Send(command);

            return this.CreateActionResult(response);
        }

        [HttpPut]
        [Route("message/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MarkMessageAsDeleted([FromBody] MarkMessageAsDeletedCommand command)
        {
            var response = await _mediator.Send(command);

            return this.CreateActionResult(response);
        }
    }
}