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
using Logic.DTO;
using Data;

namespace API
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("[controller]")]
    public class BusinessController : ControllerBase
    {
        private IBusinessRepository _repository;
        private IBusinessService _service;

        public BusinessController(IBusinessRepository repository, IBusinessService service)
        {
            _repository = repository;
            _service = service;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> NewBusiness([FromBody] CreateBusinessDTO dto)
        {
            string ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            BusinessDTO business = await _service.RegisterBusiness(dto, ownerEmail);

            return Ok(business);
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<IEnumerable<BusinessDTO>> GetAll()
        {

            return Ok(_repository.Read().ToList());
        }
    }
}