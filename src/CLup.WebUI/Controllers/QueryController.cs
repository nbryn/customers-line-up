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
using CLup.Application.Queries.User.Booking;
using CLup.Application.Queries.User.General;
using CLup.Application.Queries.User.Message;
using CLup.Application.Queries.User.NotEmployed;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLup.Application.Queries.User
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("api/query")]
    public class QueryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public QueryController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [Route("business/owner")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BusinessDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BusinessesByOwner()
        {
            var ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _mediator.Send(new BusinessesByOwnerQuery(ownerEmail));

            return this.CreateActionResult(result);
        }

        [HttpGet]
        [Route("business/all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BusinessDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllBusinesses()
        {
            var result = await _mediator.Send(new AllBusinessesQuery());

            return this.CreateActionResult(result);
        }

        [HttpGet]
        [Route("business/types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> FetchBusinessTypes()
        {
            return Ok(await _mediator.Send(new BusinessTypesQuery()));
        }

        [Route("business/insights")]
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
        [Route("business/{businessId}/employee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<EmployeeDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchEmployees([FromRoute] FetchEmployeesQuery query)
        {
            var result = await _mediator.Send(query);

            return this.CreateActionResult(result);
        }

        [HttpGet]
        [Route("business/{businessId}/timeslot")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<TimeSlotDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchTimeSlots([FromRoute] TimeSlotsByBusinessQuery query)
        {
            var result = await _mediator.Send(query);

            return this.CreateActionResult(result);
        }

        [HttpGet]
        [Route("business/timeslot/available")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<TimeSlotDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAvailableTimeSlots([FromQuery] AvailableTimeSlotsByBusinessQuery query)
        {
            var result = await _mediator.Send(query);

            return this.CreateActionResult(result);
        }

        [Route("business/{businessId}/messages")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FetchMessagesResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchBusinessMessages([FromRoute] FetchBusinessMessagesQuery query)
        {
            query.UserEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(query);

            return this.CreateActionResult(result);
        }

        [HttpGet]
        [Route("business/{businessId}/booking")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BookingDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchBusinessBookings([FromRoute] BusinessBookingsQuery query)
        {
            var response = await _mediator.Send(query);

            return this.CreateActionResult(response);
        }
        
        [Route("user/info")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchUserInfo()
        {
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(new UserInfoQuery(userEmail));

            return this.CreateActionResult(result);
        }
        
        [Route("user/notEmployedByBusiness/{businessId}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersNotEmployedByBusinessResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllUsersNotAlreadyEmployedByBusiness([FromRoute] UsersNotEmployedByBusinessQuery query)
        {
            var result = await _mediator.Send(query);

            return this.CreateActionResult(result);
        }
        
        [Route("user/all")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<UserDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllUsers()
        {
            var result = await _mediator.Send(new FetchAllUsersQuery());

            return this.CreateActionResult(result);
        }
        
        [Route("user/{userId}/messages")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FetchUserMessagesResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchMessages([FromRoute] FetchUserMessagesQuery query)
        {
            var result = await _mediator.Send(query);

            return this.CreateActionResult(result);
        }
        
        [HttpGet]
        [Route("user/{userId}/booking")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BookingDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchUserBookings([FromRoute] FetchBookingsQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }
        
        [Route("user/insights")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserBookingInsightsQuery.Model))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchUserInsights()
        {
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(new UserBookingInsightsQuery.Query(userEmail));

            return this.CreateActionResult(result);
        }
    }
}