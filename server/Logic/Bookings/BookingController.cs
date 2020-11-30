using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using Data;
using Logic.Auth;
using Logic.DTO;
using Logic.Util;

namespace Logic.Bookings
{
    [Authorize(Policy = Policies.User)]
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _repository;
        private readonly IBookingService _service;
        private readonly IDTOMapper _dtoMapper;

        public BookingController(IBookingRepository repository, 
        IBookingService service, IDTOMapper dtoMapper)
        {
            _repository = repository;
            _dtoMapper = dtoMapper;
            _service = service;
        }

        [HttpPost]
        [Route("new")]
        public async Task<IActionResult> NewBooking([FromQuery] int timeSlotId)
        {
            string userMail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            int response = await _service.CreateBooking(userMail, timeSlotId);

            return new StatusCodeResult((int)response);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> RemoveBooking([FromQuery] int timeSlotId)
        {
            string userMail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _repository.DeleteBooking(userMail, timeSlotId);

            return Ok();
        }

        [Route("user")]
        [HttpGet]
        public async Task<ICollection<TimeSlotDTO>> FetchUserBookings()
        {
            string userMail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            IList<Booking> bookings = await _repository.FindBookingsByUser(userMail);

            var dtos = bookings.Select(async x => await _dtoMapper.ConvertTimeSlotToDTO(x.TimeSlot));

            return await Task.WhenAll(dtos.ToList());
        }
    }
}