using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using Logic.Auth;
using Logic.BusinessQueues;
using Logic.DTO;
using Data;

namespace API
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("[controller]")]
    public class BusinessQueueController : ControllerBase
    {
        private readonly IBusinessQueueRepository _repository;
        private readonly IBusinessQueueService _service;

        public BusinessQueueController(IBusinessQueueRepository repository, IBusinessQueueService service)
        {
            _repository = repository;
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
        public async Task<IActionResult> FetchQueuesForBusiness(int businessId)
        {
            IEnumerable<BusinessQueueDTO> queues = await _service.GetAllQueuesForBusiness(businessId);

            return Ok(queues);
        }
    }
}