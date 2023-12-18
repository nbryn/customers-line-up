using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Auth;
using CLup.Application.Businesses;
using CLup.Application.Shared.Util;
using CLup.Application.Users;
using CLup.Domain.Businesses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Users.Queries;
using CLup.Domain.Businesses.Enums;

namespace CLup.WebUI.Controllers
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("api/query")]
    public class QueryController : ControllerBase
    {
        private readonly ICLupRepository _context;
        private readonly IMapper _mapper;

        public QueryController(
            ICLupRepository context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchUserAggregate()
        {
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _context.FetchUserAggregate(userEmail);

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
            var businesses = await _context.FetchAllBusinesses();

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersNotEmployedByBusiness))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllUsersNotAlreadyEmployedByBusiness([FromRoute] string businessId)
        {
            var business = await _context.FetchBusiness(businessId);
            if (business == null)
            {
                return NotFound();
            }

            var users = await _context.FetchUsersNotEmployedByBusiness(businessId);

            return Ok(new UsersNotEmployedByBusiness()
                { BusinessId = businessId, Users = _mapper.Map<IList<UserDto>>(users) });
        }
    }
}