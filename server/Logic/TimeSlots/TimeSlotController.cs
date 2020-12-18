using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

using Logic.Auth;
using Logic.Context;
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

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteTimeSlot(int id)
        {
            Response response = await _repository.DeleteTimeSlot(id);

            return new StatusCodeResult((int)response);
        }


        [HttpGet]
        [Route("business/{id}")]
        public async Task<ICollection<TimeSlotDTO>> FetchAllTimeSlotsForBusiness(int id)
        {
            IList<TimeSlot> timeSlots = await _repository.FindTimeSlotsByBusiness(id);

            return timeSlots.Select(x => _dtoMapper.ConvertTimeSlotToDTO(x)).ToList();
        }

        [HttpGet]
        [Route("available")]
        public async Task<ICollection<TimeSlotDTO>> FetchAllAvailableTimeSlotsForBusiness([FromQuery] AvailableTimeSlotsRequest request)
        {
            IList<TimeSlot> timeSlots = await _repository.FindAvailableTimeSlotsByBusiness(request);

            return timeSlots.Select(x => _dtoMapper.ConvertTimeSlotToDTO(x)).ToList();
        }
    }
}