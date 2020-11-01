using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

using Logic.Exceptions;
using Logic.Models;
using Logic.Services;
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterDTO user)
        {
            var id = await _service.RegisterUser(user);

            return CreatedAtAction(nameof(Get), new { id }, default);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginRequest)
        {
            try
            {
                UserDTO user = await _service.Authenticate(loginRequest);

                return CreatedAtAction(nameof(Get), new { user }, default);

            }
            catch (AuthenticationFailedException e)
            {
                return CreatedAtAction(nameof(Get), new { e.Message }, default);
            }
        }

        [HttpGet]
        public IEnumerable<UserDTO> Get()
        {
            return _repository.Read().ToList();
        }
    }
}