using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Logic.Util;
using Logic.DTO.User;
using Logic.Auth;
using Data;

namespace Logic.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IUserService _service;
        private readonly IDTOMapper _dtoMapper;

        public UserController(IUserRepository repository, IDTOMapper dtoMapper,
        IUserService service)
        {
            _repository = repository;
            _dtoMapper = dtoMapper;
            _service = service;
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO user)
        {
            LoginResponseDTO response = await _service.RegisterUser(user);

            if (response.isError)
            {
                return Conflict(new { message = $"An existing user with the email '{user.Email}' was found." });
            }

            return Ok(response);
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginRequest)
        {
            LoginResponseDTO user = await _service.AuthenticateUser(loginRequest);

            if (user.isError)
            {
                return Unauthorized();
            }

            return Ok(user);
        }

        [Authorize(Policy = Policies.User)]
        [Route("all")]
        [HttpGet]
        public async Task<IEnumerable<UserDTO>> FetchAllUsers()
        {
            var all = await _repository.GetAll();

            return all.Select(x => _dtoMapper.ConvertUserToDTO(x));
        }
    }
}