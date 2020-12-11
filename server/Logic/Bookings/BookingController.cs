using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;

using Data;
using Logic.Auth;
using Logic.DTO;
using Logic.Util;
using Logic.Context;

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

            (Response response, string message) = await _service.CreateBooking(userMail, timeSlotId);

            return new ObjectResult(message) { StatusCode = (int)response };
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> RemoveBooking([FromQuery] int timeSlotId)
        {
            string userMail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Response response = await _repository.DeleteBooking(userMail, timeSlotId);

            return new StatusCodeResult((int)response);
        }

        [Route("user")]
        [HttpGet]
        public async Task<ICollection<TimeSlotDTO>> FetchUserBookings()
        {
            string userMail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            IList<Booking> bookings = await _repository.FindBookingsByUser(userMail);

            return bookings.Select(x => _dtoMapper.ConvertTimeSlotToDTO(x.TimeSlot)).ToList();
        }

        [Route("business")]
        [HttpGet]
        public async Task<ICollection<BookingDTO>> FetchBusinessBookings([FromQuery] int businessId)
        {       
            IList<Booking> bookings = await _repository.FindBookingsByBusiness(businessId);

            return bookings.Select(x => _dtoMapper.ConvertBookingToDTO(x)).ToList();
        }
    }
}