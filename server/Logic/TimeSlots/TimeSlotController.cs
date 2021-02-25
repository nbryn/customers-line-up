
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Data;
using Logic.Auth;
using Logic.DTO;
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

        public TimeSlotController(
            ITimeSlotRepository repository,
            ITimeSlotService service, 
            IDTOMapper dtoMapper)
        {
            _repository = repository;
            _dtoMapper = dtoMapper;
            _service = service;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> NewTimeSlot([FromBody] GenerateTimeSlotsRequest dto)
        {
            var response = await _service.GenerateTimeSlots(dto);

            return new ObjectResult(response._message) { StatusCode = (int)response._statusCode };
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTimeSlot(int id)
        {
            var response = await _repository.DeleteTimeSlot(id);

            return new StatusCodeResult((int)response);
        }


        [HttpGet]
        [Route("business/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<TimeSlotDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllTimeSlotsForBusiness(int id)
        {
            var response = await _repository.FindTimeSlotsByBusiness(id);

            if (response == null)
            {
                return NotFound();
            }

            var timeSlots = response.Select(x => _dtoMapper.ConvertTimeSlotToDTO(x)).ToList();

            return Ok(timeSlots);
        }

        [HttpGet]
        [Route("available")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<TimeSlotDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllAvailableTimeSlotsForBusiness([FromQuery] AvailableTimeSlotsRequest request)
        {
            var response = await _repository.FindAvailableTimeSlotsByBusiness(request);

            if (response == null)
            {
                return NotFound();
            }

            var timeSlots = response.Select(x => _dtoMapper.ConvertTimeSlotToDTO(x)).ToList();

            return Ok(timeSlots);
        }
    }
}