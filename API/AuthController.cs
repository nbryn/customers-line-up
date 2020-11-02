using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


using Logic.Exceptions;
using Logic.Models;
using Logic.Services;
using Data;

namespace API
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {

            _service = service;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO user)
        {
            LoginResponseDTO response = await _service.RegisterUser(user);

            return CreatedAtAction("Get", new { response }, default);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginRequest)
        {
            try
            {
                LoginResponseDTO user = await _service.Authenticate(loginRequest);

                return CreatedAtAction("Get", new { user }, default);

            }
            catch (AuthenticationFailedException e)
            {
                return CreatedAtAction("Get", new { e.Message }, default);
            }
        }
    }
}