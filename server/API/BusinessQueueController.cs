using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;

using Logic.Auth;
using Logic.BusinessQueues;
using Logic.DTO;
using Data;
using Logic.Util;

namespace API
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("[controller]")]
    public class BusinessQueueController : ControllerBase
    {
        private readonly IBusinessQueueRepository _repository;
        private readonly IBusinessQueueService _service;

        private readonly IDTOMapper _dtoMapper;

        public BusinessQueueController(IBusinessQueueRepository repository,
        IBusinessQueueService service, IDTOMapper dtoMapper)
        {
            _repository = repository;
            _dtoMapper = dtoMapper;
            _service = service;

        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> NewQueue([FromBody] CreateBusinessQueueRequest dto)
        {
            IEnumerable<BusinessQueueDTO> queues = await _service.GenerateQueues(dto);

            return Ok(queues);
        }

        [HttpPut]
        [Route("adduser")]
        public async Task<IActionResult> AddUserToQueue([FromBody] AddUserToQueueRequest dto)
        {
            dto.UserMail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var response = await _service.AddUserToQueue(dto);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [Route("business")]
        public async Task<ICollection<BusinessQueueDTO>> FetchAllQueuesForBusiness([FromQuery] int id)
        {
            Console.WriteLine(id);
            IList<BusinessQueue> queues = await _repository.FindQueuesByBusiness(id);

            return queues.Select(x => _dtoMapper.ConvertQueueToDTO(x)).ToList();
        }
        
        [HttpGet]
        [Route("available")]
        public async Task<ICollection<BusinessQueueDTO>> FetchAllAvailableQueuesForBusiness([FromBody] AvailableQueuesRequest request)
        {
            IList<BusinessQueue> queues = await _repository.FindAvailableQueuesByBusiness(request);

            return queues.Select(x => _dtoMapper.ConvertQueueToDTO(x)).ToList();
        }
    }
}