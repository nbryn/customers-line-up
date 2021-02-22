using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
                                 IBookingService service, 
                                 IDTOMapper dtoMapper)
        {
            _repository = repository;
            _dtoMapper = dtoMapper;
            _service = service;
        }

        [HttpPost]
        [Route("{timeSlotId}")]
        public async Task<IActionResult> NewBooking(int timeSlotId)
        {
            string userMail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var response = await _service.VerifyNewBooking(userMail, timeSlotId);

            return new ObjectResult(response._message) { StatusCode = (int)response._statusCode };
        }

        [HttpDelete]
        [Route("user/{timeSlotId}")]
        public async Task<IActionResult> RemoveBookingForUser(int timeSlotId)
        {
            string userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            HttpCode response = await _repository.DeleteBooking(userEmail, timeSlotId);

            return new StatusCodeResult((int)response);
        }

        [HttpDelete]
        [Route("business/{timeSlotId}")]
        public async Task<IActionResult> RemoveBookingForBusiness(int timeSlotId, [FromQuery] string userEmail)
        {
            string ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            HttpCode response = await _service.VerifyDeleteBookingRequest(ownerEmail, userEmail, timeSlotId);

            return new StatusCodeResult((int)response);
        }

        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> FetchUserBookings()
        {
            string userMail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var response = await _repository.FindBookingsByUser(userMail);

            if (response == null)
            {
                return NotFound();
            }

            var bookings = response.Select(x => _dtoMapper.ConvertBookingToDTO(x)).ToList();

            return Ok(bookings);
        }

        [HttpGet]
        [Route("business/{businessId}")]
        public async Task<IActionResult> FetchBusinessBookings(int businessId)
        {
            var response = await _repository.FindBookingsByBusiness(businessId);

            if (response == null)
            {
                return NotFound();
            }

            var bookings = response.Select(x => _dtoMapper.ConvertBookingToDTO(x)).ToList();

            return Ok(bookings);
        }
    }
}