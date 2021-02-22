using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Data;
using Logic.Auth;
using Logic.DTO;
using Logic.Util;


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
                                  IBusinessService service, 
                                  IDTOMapper dtoMapper)
        {
            _repository = repository;
            _dtoMapper = dtoMapper;
            _service = service;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> NewBusiness([FromBody] NewBusinessRequest dto)
        {
            string ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            dto.OwnerEmail = ownerEmail;

            var response = await _service.RegisterBusiness(dto);

            return new StatusCodeResult((int)response);
        }

        [HttpGet]
        [Route("owner")]
        public async Task<IActionResult> FetchBusinessesForOwner()
        {
            string ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var response = await _repository.FindBusinessesByOwner(ownerEmail);

            if (response == null)
            {
                return NotFound();
            }

            var businesses = response.Select(x => _dtoMapper.ConvertBusinessToDTO(x));

            return Ok(businesses);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBusinessData(int id, [FromBody] NewBusinessRequest dto)
        {
            HttpCode response = await _repository.UpdateBusiness(id, dto);

            return new StatusCodeResult((int)response);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> FetchAll()
        {
            var response = await _repository.GetAll();

            if (response == null)
            {
                return NotFound();
            }

            var businesses = response.Select(x => _dtoMapper.ConvertBusinessToDTO(x));

            return Ok(businesses);
        }

        [HttpGet]
        [Route("types")]
        public IActionResult FetchBusinessTypes()
        {
            return Ok(_dtoMapper.GetBusinessTypes());
        }
    }
}