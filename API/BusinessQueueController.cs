using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        private readonly IBusinessQueueService _service;

        public BusinessQueueController(IBusinessQueueService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> NewQueue([FromBody] CreateBusinessQueueRequest dto)
        {
            
            IEnumerable<BusinessQueueDTO> queues = await _service.GenerateQueues(dto);

            return Ok(queues);
        }
    }
}