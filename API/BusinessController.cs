using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using Logic.Auth;
using Logic.Businesses;
using Logic.Businesses.Models;
using Data;

namespace API
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("[controller]")]
    public class BusinessController : ControllerBase
    {
        private IBusinessService _service;

        public BusinessController(IBusinessService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> NewBusiness([FromBody] CreateBusinessDTO dto)
        {
            string ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            BusinessDTO business = await _service.RegisterBusiness(ownerEmail, dto);

            return Ok(business);
        }
    }
}