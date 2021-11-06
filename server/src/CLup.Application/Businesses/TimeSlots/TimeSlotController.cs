using System.Collections.Generic;
using System.Threading.Tasks;
using CLup.Application.Auth;
using CLup.Application.Businesses.TimeSlots.Commands;
using CLup.Application.Businesses.TimeSlots.Queries;
using CLup.Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLup.Application.Businesses.TimeSlots
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("[controller]")]
    public class TimeSlotController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TimeSlotController(IMediator mediator) =>  _mediator = mediator;

        [HttpPost]
        [Route("generate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GenerateTimeSlots([FromBody] GenerateTimeSlotsCommand command)
        {
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTimeSlot([FromRoute] DeleteTimeSlotCommand command)
        {
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [HttpGet]
        [Route("business/{businessId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<TimeSlotDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> TimeSlotsByBusiness([FromRoute] TimeSlotsByBusinessQuery query)
        {
            var result = await _mediator.Send(query);

            return this.CreateActionResult(result);
        }

        [HttpGet]
        [Route("available")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<TimeSlotDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AvailableTimeSlotsByBusiness([FromQuery] AvailableTimeSlotsByBusinessQuery query)
        {
            var result = await _mediator.Send(query);

            return this.CreateActionResult(result);
        }
    }
}