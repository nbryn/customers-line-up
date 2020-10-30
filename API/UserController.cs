using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Post([FromBody] UserDTO user)
        {
            var id = await _service.RegisterUser(user);

            return CreatedAtAction(nameof(Get), new { id }, default);
        }

        [HttpGet]
        public IEnumerable<UserDTO> Get()
        {
            return _repository.Read().ToList();
        }
    }
}