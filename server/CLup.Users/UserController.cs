using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

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
        private readonly IUserRepository _repository;
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(
            IUserRepository repository,
            IUserService service,
            IMapper mapper)
        {
            _repository = repository;
            _service = service;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] NewUserRequest user)
        {
            var response = await _service.RegisterUser(user);

            return this.CreateActionResult(response); ;
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var response = await _service.AuthenticateUser(loginRequest);

            return this.CreateActionResult(response);
        }

        [Authorize(Policy = Policies.User)]
        [Route("")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchUserInfo()
        {
            string userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            User user = await _repository.FindUserByEmail(userEmail);

            if (user == null)
            {
                return NotFound();
            }

            await _service.DetermineRole(user);

            return Ok(_mapper.Map<UserDTO>(user));
        }

        [Authorize(Policy = Policies.User)]
        [Route("all/{businessId}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<UserDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllUsersNotAlreadyEmployedByBusiness(int businessId)
        {
            var response = await _service.FilterUsersByBusiness(businessId);

            return this.CreateActionResult(response);
        }

        [Authorize(Policy = Policies.User)]
        [Route("all")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<UserDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllUsers()
        {
            var response = await _repository.GetAll();

            return this.CreateActionResult(response);
        }
    }
}