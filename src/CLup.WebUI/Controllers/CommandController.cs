using System.Security.Claims;
using System.Threading.Tasks;
using CLup.Application.Auth;
using CLup.Application.Businesses.Bookings.Commands.Delete;
using CLup.Application.Businesses.Commands.Create;
using CLup.Application.Businesses.Commands.Update;
using CLup.Application.Businesses.Employees.Commands.Create;
using CLup.Application.Businesses.Employees.Commands.Delete;
using CLup.Application.Businesses.TimeSlots.Commands.Delete;
using CLup.Application.Businesses.TimeSlots.Commands.Generate;
using CLup.Application.Messages.MarkAsDeleted;
using CLup.Application.Messages.Send;
using CLup.Application.Shared.Extensions;
using CLup.Application.Users.Bookings.Commands.Create;
using CLup.Application.Users.Bookings.Commands.Delete;
using CLup.Application.Users.Commands;
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
            var ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var response = await _mediator.Send(new BusinessDeleteBookingCommand(ownerEmail, bookingId, businessId));

            return this.CreateActionResult(response);
        }

        [HttpPost]
        [Route("business/employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand command)
        {
            command.OwnerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [HttpDelete]
        [Route("business/employee/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveEmployee(string email, [FromQuery] string businessId)
        {
            var ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(new DeleteEmployeeCommand(ownerEmail,businessId, email));

            return this.CreateActionResult(result);
        }

        [HttpPost]
        [Route("business/timeslot/generate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GenerateTimeSlots([FromBody] GenerateTimeSlotsCommand command)
        {
            command.OwnerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [HttpDelete]
        [Route("business/timeslot/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTimeSlot([FromRoute] DeleteTimeSlotCommand command)
        {
            command.OwnerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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
            command.UserEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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