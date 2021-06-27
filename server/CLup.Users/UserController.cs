using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CLup.Auth;
using CLup.Extensions;
using CLup.Users.DTO;
using CLup.Users.Interfaces;

namespace CLup.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _repository;
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(
            IMediator mediator,
            IUserRepository repository,
            IUserService service,
            IMapper mapper)
        {
            _mediator = mediator;
            _repository = repository;
            _service = service;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterUser.Command command)
        {
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] Login.Command command)
        {
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [Authorize(Policy = Policies.User)]
        [Route("")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchUserInfo()
        {
            string userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _mediator.Send(new UserInfo.Command(userEmail));

            return this.CreateActionResult(result);
        }

        [Authorize(Policy = Policies.User)]
        [Route("all/{businessId}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<UserDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllUsersNotAlreadyEmployedByBusiness(UsersNotEmployedByBusiness.Query query)
        {
            var result = await _mediator.Send(query);

            return this.CreateActionResult(result);
        }

        [Authorize(Policy = Policies.User)]
        [Route("all")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<UserDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllUsers()
        {
            var response = await _mediator.Send(AllUsers.Query)

            return this.CreateActionResult<IList<UserDTO>>(response);
        }

        [Authorize(Policy = Policies.User)]
        [Route("insights")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<UserDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserInsights()
        {
            string userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var response = await _service.FetchUserInsights(userEmail);

            return this.CreateActionResult<UserInsightsDTO>(response);
        }
    }
}