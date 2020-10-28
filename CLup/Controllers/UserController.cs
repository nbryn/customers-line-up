using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using CLup.Models;
using CLup.Services;
using CLup.Repositories;

namespace CLup.Controllers
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
            var id = await _repository.Create(user);

            return CreatedAtAction(nameof(Get), new { id }, default);
        }

        [HttpGet]
        public async Task<IEnumerable<UserDTO>> Get()
        {
            return null;
        }
    }
}