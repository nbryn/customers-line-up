using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;

using Logic.Auth;
using Logic.DTO;
using Data;
using Logic.Util;

namespace Logic.TimeSlots
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("[controller]")]
    public class TimeSlotController : ControllerBase
    {
        private readonly ITimeSlotRepository _repository;
        private readonly ITimeSlotService _service;
        private readonly IDTOMapper _dtoMapper;

        public TimeSlotController(ITimeSlotRepository repository,
        ITimeSlotService service, IDTOMapper dtoMapper)
        {
            _repository = repository;
            _dtoMapper = dtoMapper;
            _service = service;

        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> NewTimeSlot([FromBody] CreateTimeSlotRequest dto)
        {
            IEnumerable<TimeSlotDTO> timeSlots = await _service.GenerateTimeSlots(dto);

            return Ok(timeSlots);
        }


        [HttpGet("{id}")]
        [Route("business")]
        public async Task<ICollection<TimeSlotDTO>> FetchAllTimeSlotsForBusiness([FromQuery] int id)
        {
            IList<TimeSlot> timeSlots = await _repository.FindTimeSlotsByBusiness(id);

            var dtos = timeSlots.Select(async x => await _dtoMapper.ConvertTimeSlotToDTO(x));

            return await Task.WhenAll(dtos.ToList());
        }

        [HttpGet]
        [Route("available")]
        public async Task<ICollection<TimeSlotDTO>> FetchAllAvailableTimeSlotsForBusiness([FromQuery] AvailableTimeSlotsRequest request)
        {
            IList<TimeSlot> timeSlots = await _repository.FindAvailableTimeSlotsByBusiness(request);

            var dtos = timeSlots.Select(async x => await _dtoMapper.ConvertTimeSlotToDTO(x));

            return await Task.WhenAll(dtos.ToList());
        }
    }
}