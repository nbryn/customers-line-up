
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

using CLup.Auth;
using CLup.Extensions;
using CLup.TimeSlots.DTO;
using CLup.TimeSlots.Interfaces;

namespace CLup.TimeSlots
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("[controller]")]
    public class TimeSlotController : ControllerBase
    {
        private readonly ITimeSlotRepository _repository;
        private readonly ITimeSlotService _service;

        public TimeSlotController(
            ITimeSlotRepository repository,
            ITimeSlotService service)
        {
            _repository = repository;
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

            return this.CreateActionResult(response);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTimeSlot(int id)
        {
            var response = await _repository.DeleteTimeSlot(id);

            return this.CreateActionResult(response);
        }

        [HttpGet]
        [Route("business/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<TimeSlotDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllTimeSlotsForBusiness(int id)
        {
            var response = await _repository.FindTimeSlotsByBusiness(id);

            return this.CreateActionResult(response);
        }

        [HttpGet]
        [Route("available")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<TimeSlotDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllAvailableTimeSlotsForBusiness([FromQuery] AvailableTimeSlotsRequest request)
        {
            var response = await _repository.FindAvailableTimeSlotsByBusiness(request);

            return this.CreateActionResult(response);
        }
    }
}