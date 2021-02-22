using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Data;
using Logic.Auth;
using Logic.DTO;
using Logic.DTO.User;
using Logic.Util;



namespace Logic.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IUserService _service;
        private readonly IDTOMapper _dtoMapper;

        public UserController(
            IUserRepository repository,
            IDTOMapper dtoMapper,
            IUserService service
            )
        {
            _repository = repository;
            _dtoMapper = dtoMapper;
            _service = service;
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] NewUserRequest user)
        {
            var response = await _service.RegisterUser(user);

            if (response._statusCode == HttpCode.Conflict)
            {
                return Conflict(new { message = response._message });
            }

            return Ok(response);
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var response = await _service.AuthenticateUser(loginRequest);

            return new ObjectResult(response._message) { StatusCode = (int)response._statusCode };
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

            Role role = await _service.DetermineRole(user);

            UserDTO dto = _dtoMapper.ConvertUserToDTO(user);

            dto.Role = role.ToString();

            return Ok(dto);
        }

        [Authorize(Policy = Policies.User)]
        [Route("all/{businessId}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllUsersNotAlreadyEmployedByBusiness(int businessId)
        {
            var response = await _service.FilterUsersByBusiness(businessId);

            if (response == null)
            {
                return NotFound();
            }

            var employees = response.Select(x => _dtoMapper.ConvertUserToDTO(x));

            return Ok(employees);
        }

        [Authorize(Policy = Policies.User)]
        [Route("all")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllUsers()
        {
            var response = await _repository.GetAll();

            if (response == null)
            {
                return NotFound();
            }

            var users = response.Select(x => _dtoMapper.ConvertUserToDTO(x));

            return Ok(users);
        }
    }
}