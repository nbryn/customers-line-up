using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Auth;
using CLup.Application.Businesses;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Util;
using CLup.Application.Users;
using CLup.Domain.Businesses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLup.WebUI.Controllers
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("api/query")]
    public class QueryController : ControllerBase
    {
        private readonly IReadOnlyDbContext _readContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public QueryController(
            IReadOnlyDbContext readContext, 
            IMediator mediator,
            IMapper mapper)
        {
            _readContext = readContext;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchUserAggregate()
        {
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _readContext.FetchUserAggregate(userEmail);

            if (user == null)
            {
                return NotFound("User was not found");
            }

            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpGet]
        [Route("business/all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BusinessDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllBusinesses()
        {
            var businesses = await _readContext.FetchAllBusinesses();

            return Ok(_mapper.Map<IList<BusinessDto>>(businesses));
        }

        [HttpGet]
        [Route("business/types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult FetchBusinessTypes()
        {
            var types = EnumUtil
                .GetValues<BusinessType>()
                .Select(type => type.ToString("G"))
                .ToList();

            return Ok(types);
        }

        [Route("user/notEmployedByBusiness/{businessId}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersNotEmployedByBusiness.Model))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllUsersNotAlreadyEmployedByBusiness([FromRoute] UsersNotEmployedByBusiness.Query query)
        {
            var result = await _mediator.Send(query);

            return this.CreateActionResult(result);
        }

        [Route("business/insights")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserBusinessInsights.Model))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchBusinessInsights()
        {
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(new UserBusinessInsights.Query(userEmail));

            return this.CreateActionResult(result);
        }

        [Route("user/insights")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserBookingInsights.Model))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchUserInsights()
        {
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(new UserBookingInsights.Query(userEmail));

            return this.CreateActionResult(result);
        }
    }
}