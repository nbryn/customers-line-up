using System.Linq;
using System.Threading.Tasks;

using CLup.Bookings.Interfaces;
using CLup.Businesses;
using CLup.Businesses.Interfaces;
using CLup.Util;
using CLup.TimeSlots;
using CLup.TimeSlots.Interfaces;

namespace CLup.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly ITimeSlotRepository _timeSlotRepository;
        private readonly IBookingRepository _bookingRepository;

        public BookingService(
            IBusinessRepository businessRepository,
            ITimeSlotRepository timeSlotRepository,
            IBookingRepository bookingRepository)
        {
            _businessRepository = businessRepository;
            _timeSlotRepository = timeSlotRepository;
            _bookingRepository = bookingRepository;
        }
        public async Task<ServiceResponse> VerifyNewBooking(string userEmail, string timeSlotId)
        {
            Booking bookingExists = await _bookingRepository.FindBookingByUserAndTimeSlot(userEmail, timeSlotId);

            if (bookingExists != null)
            {
                return new ServiceResponse(HttpCode.Conflict, "You already have a booking for this time slot");

            }

            TimeSlot timeSlot = await _timeSlotRepository.FindTimeSlotById(timeSlotId);

            if (timeSlot == null)
            {
                return new ServiceResponse(HttpCode.NotFound, "Time slot does not exist");
            }

            if (timeSlot.Bookings.Count() >= timeSlot.Capacity)
            {
                return new ServiceResponse(HttpCode.Conflict, "This time slot is full");
            }

            Business business = await _businessRepository.FindBusinessById(timeSlot.BusinessId);

            if (business == null)
            {
                //Handle business does not exists - Gather null checks in one place?
            }

            Booking booking = new Booking { UserEmail = userEmail, TimeSlotId = timeSlotId, BusinessId = business.Id.ToString() };

            var response = await _bookingRepository.SaveBooking(booking);

            return new ServiceResponse(HttpCode.Created, "Booking successfull");
        }

        public async Task<ServiceResponse> VerifyDeleteBookingRequest(string ownerEmail, string userEmail, string timeSlotId)
        {
            Booking booking = await _bookingRepository.FindBookingByUserAndTimeSlot(userEmail, timeSlotId);

            if (booking == null)
            {
                return new ServiceResponse(HttpCode.NotFound);
            }

            Business business = await _businessRepository.FindBusinessById(booking.TimeSlot.BusinessId);

            if (business.OwnerEmail != ownerEmail)
            {
                return new ServiceResponse(HttpCode.Forbidden);
            }

            var response = await _bookingRepository.DeleteBooking(userEmail, timeSlotId);

            return response;
        }

    }
}