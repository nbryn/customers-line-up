using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CLup.Application.Auth;
using CLup.Application.Queries.Business.Booking;
using CLup.Application.Queries.Business.Employee.Models;
using CLup.Application.Queries.Business.General;
using CLup.Application.Queries.Business.Message;
using CLup.Application.Queries.Business.Owner;
using CLup.Application.Queries.Business.TimeSlot.All;
using CLup.Application.Queries.Business.TimeSlot.Available;
using CLup.Application.Queries.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLup.Application.Queries.Business
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("query/business")]
    public class BusinessQueryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BusinessQueryController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [Route("owner")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BusinessDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BusinessesByOwner()
        {
            var ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _mediator.Send(new BusinessesByOwnerQuery(ownerEmail));

            return this.CreateActionResult(result);
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BusinessDto>))]
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

        [Route("{businessId}/messages")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FetchMessagesResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchMessages([FromRoute] FetchMessagesQuery query)
        {
            query.UserEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(query);

            return this.CreateActionResult(result);
        }

        [HttpGet]
        [Route("booking/{businessId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BookingDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchBookings([FromRoute] BusinessBookingsQuery query)
        {
            var response = await _mediator.Send(query);

            return this.CreateActionResult(response);
        }
    }
}