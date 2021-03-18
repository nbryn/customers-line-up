using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using CLup.Auth;
using CLup.Businesses.DTO;
using CLup.Businesses.Interfaces;
using CLup.Extensions;
using CLup.Util;

namespace CLup.Businesses
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("[controller]")]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessRepository _repository;
        private readonly IBusinessService _service;

        public BusinessController(
            IBusinessRepository repository,
            IBusinessService service)
        {
            _repository = repository;
            _service = service;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> NewBusiness([FromBody] NewBusinessRequest dto)
        {
            string ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            dto.OwnerEmail = ownerEmail;

            var response = await _service.RegisterBusiness(dto);

            return this.CreateActionResult(response);
        }

        [HttpGet]
        [Route("owner")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BusinessDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchBusinessesForOwner()
        {
            string ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var response = await _repository.FindBusinessesByOwner(ownerEmail);

            return this.CreateActionResult<IList<BusinessDTO>>(response);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBusinessData(int id, [FromBody] NewBusinessRequest dto)
        {
            var response = await _repository.UpdateBusiness(id, dto);

            return this.CreateActionResult(response);
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BusinessDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAll()
        {
            var response = await _repository.GetAll();

            return this.CreateActionResult<IList<BusinessDTO>>(response);
        }

        [HttpGet]
        [Route("types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult FetchBusinessTypes()
        {
            return Ok(_service.GetBusinessTypes());
        }
    }
}