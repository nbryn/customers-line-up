using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

using Logic.Auth;
using Logic.Context;
using Logic.DTO;
using Logic.Util;
using Data;

namespace Logic.Businesses
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("[controller]")]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessRepository _repository;
        private readonly IBusinessService _service;
        private readonly IDTOMapper _dtoMapper;

        public BusinessController(IBusinessRepository repository,
        IBusinessService service, IDTOMapper dtoMapper)
        {
            _repository = repository;
            _dtoMapper = dtoMapper;
            _service = service;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> NewBusiness([FromBody] NewBusinessDTO dto)
        {
            string ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            dto.OwnerEmail = ownerEmail;

            BusinessDTO business = await _service.RegisterBusiness(dto);

            return Ok(business);
        }

        [HttpGet]
        [Route("owner")]
        public async Task<IEnumerable<BusinessDTO>> FetchBusinessesForOwner()
        {
            string ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var all = await _repository.FindBusinessesByOwner(ownerEmail);

            return all.Select(x => _dtoMapper.ConvertBusinessToDTO(x));
        }

        [HttpPut]
        [Route("/{id}")]
        public async Task<IActionResult> UpdateBusinessData(int id, [FromBody] NewBusinessDTO dto)
        {
            Response response = await _repository.UpdateBusiness(id, dto);

            return new StatusCodeResult((int)response);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<BusinessDTO>> FetchAll()
        {
            var all = await _repository.GetAll();

            return all.Select(x => _dtoMapper.ConvertBusinessToDTO(x));
        }

        [HttpGet]
        [Route("types")]
        public IEnumerable<string> FetchBusinessTypes()
        {
            return _dtoMapper.GetBusinessTypes();
        }
    }
}