using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Logic.Exceptions;
using Logic.Models;
using Logic.Users;
using Logic.Auth;
using Data;

namespace API
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
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

        [HttpGet]
        [Route("all")]
        public ActionResult<IEnumerable<UserDTO>> Get()
        {
            return Ok(_repository.Read().ToList());
        }
    }
}