using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CLup.Application.Auth;
using CLup.Application.Bookings.Commands.CreateBooking;
using CLup.Application.Bookings.Commands.UserDeleteBooking;
using CLup.Application.Businesses.Commands.CreateBusiness;
using CLup.Application.Businesses.Commands.UpdateBusiness;
using CLup.Application.Employees.Commands.CreateEmployee;
using CLup.Application.Employees.Commands.DeleteEmployee;
using CLup.Application.Messages.Commands.MarkMessageAsDeleted;
using CLup.Application.Messages.Commands.SendMessage;
using CLup.Application.Shared.Extensions;
using CLup.Application.TimeSlots.Commands.DeleteTimeSlot;
using CLup.Application.TimeSlots.Commands.GenerateTimeSlot;
using CLup.Application.Users.Commands.UpdateUserInfo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLup.WebUI.Controllers
{
    using Application.Bookings.Commands.BusinessDeleteBooking;

    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("api")]
    public class CommandController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("business")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateBusiness([FromBody] CreateBusinessCommand command)
        {
            command.OwnerId = Guid.Parse((ReadOnlySpan<char>)User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [HttpPut]
        [Route("business/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBusinessData([FromBody] UpdateBusinessCommand command)
        {
            command.OwnerId = Guid.Parse((ReadOnlySpan<char>)User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [HttpDelete]
        [Route("business/{businessId}/booking")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBusinessBooking(Guid businessId, [FromQuery] Guid bookingId)
        {
            var ownerId = Guid.Parse((ReadOnlySpan<char>)User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = await _mediator.Send(new BusinessDeleteBookingCommand(ownerId, bookingId, businessId));

            return this.CreateActionResult(response);
        }

        [HttpPost]
        [Route("business/employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand command)
        {
            command.OwnerId = Guid.Parse((ReadOnlySpan<char>)User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [HttpDelete]
        [Route("business/employee/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveEmployee(Guid userId, [FromQuery] Guid businessId)
        {
            var ownerId = Guid.Parse((ReadOnlySpan<char>)User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(new DeleteEmployeeCommand(ownerId, businessId, userId));

            return this.CreateActionResult(result);
        }

        [HttpPost]
        [Route("business/timeslot/generate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GenerateTimeSlots([FromBody] GenerateTimeSlotsCommand command)
        {
            command.OwnerId = Guid.Parse((ReadOnlySpan<char>)User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [HttpDelete]
        [Route("business/timeslot/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTimeSlot([FromRoute] DeleteTimeSlotCommand command)
        {
            command.OwnerId = Guid.Parse((ReadOnlySpan<char>)User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
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
        public async Task<IActionResult> CreateBooking(Guid timeSlotId, [FromQuery] Guid userId, [FromQuery] Guid businessId)
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
            command.UserId = Guid.Parse((ReadOnlySpan<char>)User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
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
            command.UserId = Guid.Parse((ReadOnlySpan<char>)User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var response = await _mediator.Send(command);

            return this.CreateActionResult(response);
        }
    }
}
