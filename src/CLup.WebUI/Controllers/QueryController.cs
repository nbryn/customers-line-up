using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Auth;
using CLup.Application.Businesses;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Util;
using CLup.Application.Users;
using CLup.Domain.Businesses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CLup.Application.Shared.Extensions;

namespace CLup.WebUI.Controllers
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("api/query")]
    public class QueryController : ControllerBase
    {
        private readonly IReadOnlyDbContext _readContext;
        private readonly IMapper _mapper;

        public QueryController(
            IReadOnlyDbContext readContext,
            IMapper mapper)
        {
            _readContext = readContext;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersNotEmployedByBusiness))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllUsersNotAlreadyEmployedByBusiness([FromRoute] string businessId)
        {
            var result = await _readContext.FetchUsersNotEmployedByBusiness(businessId);

            return this.CreateActionResult(result);
        }
    }
}