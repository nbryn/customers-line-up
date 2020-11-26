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
        public async Task<IActionResult> AddUserToQueue([FromQuery] int queueId)
        {
            string userMail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var response = await _service.AddUserToQueue(userMail, queueId);

            return Ok(response);
        }

        [HttpDelete]
        [Route("removeuser")]
        public async Task<IActionResult> RemoveUserFromQueue([FromQuery] int queueId)
        {
            string userMail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var response = await _service.RemoverUserFromQueue(userMail, queueId);

            return Ok(response);
        }

        [Route("user")]
        public async Task<ICollection<BusinessQueueDTO>> FetchQueuesForUser()
        {
            string userMail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            IList<BusinessQueue> queues = await _repository.FindQueuesByUser(userMail);

            var dtos = queues.Select(async x => await _dtoMapper.ConvertQueueToDTO(x));

            return await Task.WhenAll(dtos.ToList());
        }

        [HttpGet("{id}")]
        [Route("business")]
        public async Task<ICollection<BusinessQueueDTO>> FetchAllQueuesForBusiness([FromQuery] int id)
        {
            IList<BusinessQueue> queues = await _repository.FindQueuesByBusiness(id);

            var dtos = queues.Select(async x => await _dtoMapper.ConvertQueueToDTO(x));

            return await Task.WhenAll(dtos.ToList());
        }

        [HttpGet]
        [Route("available")]
        public async Task<ICollection<BusinessQueueDTO>> FetchAllAvailableQueuesForBusiness([FromQuery] AvailableQueuesRequest request)
        {
            IList<BusinessQueue> queues = await _repository.FindAvailableQueuesByBusiness(request);

            var dtos = queues.Select(async x => await _dtoMapper.ConvertQueueToDTO(x));

            return await Task.WhenAll(dtos.ToList());
        }
    }
}