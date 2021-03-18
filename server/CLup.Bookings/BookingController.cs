using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using CLup.Auth;
using CLup.Bookings.DTO;
using CLup.Bookings.Interfaces;
using CLup.Extensions;

namespace CLup.Bookings
{
    [Authorize(Policy = Policies.User)]
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _repository;
        private readonly IBookingService _service;

        public BookingController(
            IBookingRepository repository,
            IBookingService service)
        {
            _repository = repository;
            _service = service;
        }

        [HttpPost]
        [Route("{timeSlotId}")]
        public async Task<IActionResult> NewBooking(int timeSlotId)
        {
            string userMail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var response = await _service.VerifyNewBooking(userMail, timeSlotId);

            return this.CreateActionResult(response);
        }

        [HttpDelete]
        [Route("user/{timeSlotId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveBookingForUser(int timeSlotId)
        {
            string userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var response = await _repository.DeleteBooking(userEmail, timeSlotId);

            return this.CreateActionResult(response);
        }

        [HttpDelete]
        [Route("business/{timeSlotId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveBookingForBusiness(int timeSlotId, [FromQuery] string userEmail)
        {
            string ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var response = await _service.VerifyDeleteBookingRequest(ownerEmail, userEmail, timeSlotId);

            return this.CreateActionResult(response);
        }

        [HttpGet]
        [Route("user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BookingDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchUserBookings()
        {
            string userMail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var response = await _repository.FindBookingsByUser(userMail);

            return this.CreateActionResult<IList<BookingDTO>>(response);
        }

        [HttpGet]
        [Route("business/{businessId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BookingDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchBusinessBookings(int businessId)
        {
            var response = await _repository.FindBookingsByBusiness(businessId);

            return this.CreateActionResult<IList<BookingDTO>>(response);
        }
    }
}