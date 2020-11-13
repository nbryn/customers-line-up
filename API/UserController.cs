
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using Logic.Exceptions;
using Logic.DTO.User;
using Logic.Users;
using Logic.Auth;
using Data;

namespace API
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IUserService _service;

        public UserController(IUserRepository repository, IUserService service)
        {
            _repository = repository;
            _service = service;
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO user)
        {
            LoginResponseDTO response = await _service.RegisterUser(user);

            return Ok(response);
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginRequest)
        {
            LoginResponseDTO user = await _service.AuthenticateUser(loginRequest);

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user);
        }

        [Authorize(Policy = Policies.User)]
        [Route("all")]
        [HttpGet]

        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            return Ok(await _repository.Read().ToListAsync());
        }
    }
}