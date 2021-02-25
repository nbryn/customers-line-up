using System.Threading.Tasks;

using Data;
using Logic.Businesses;
using Logic.DTO;
using Logic.TimeSlots;

namespace Logic.Bookings
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
        public async Task<QueryResult> VerifyNewBooking(string userEmail, int timeSlotId)
        {
            Booking bookingExists = await _bookingRepository.FindBookingByUserAndTimeSlot(userEmail, timeSlotId);

            if (bookingExists != null)
            {
                return new QueryResult(HttpCode.Conflict, "You already have a booking for this time slot");
                
            }

            TimeSlot timeSlot = await _timeSlotRepository.FindTimeSlotById(timeSlotId);

            if (timeSlot == null)
            {
                return new QueryResult(HttpCode.NotFound, "Time slot does not exist");
            }

            if (timeSlot.Bookings?.Count >= timeSlot.Capacity)
            {
                return new QueryResult(HttpCode.Conflict, "This time slot is full");
            }

            Business business = await _businessRepository.FindBusinessById(timeSlot.BusinessId);

            if (business == null)
            {
                //Handle business does not exists - Gather null checks in one place?
            }

            Booking booking = new Booking { UserEmail = userEmail, TimeSlotId = timeSlotId, BusinessId = business.Id };

            var response = await _bookingRepository.SaveBooking(booking);

            return new QueryResult(HttpCode.Created, "Booking successfull");
        }

        public async Task<HttpCode> VerifyDeleteBookingRequest(string ownerEmail, string userEmail, int timeSlotId)
        {
            Booking booking = await _bookingRepository.FindBookingByUserAndTimeSlot(userEmail, timeSlotId);

            if (booking == null)
            {
                return HttpCode.NotFound;
            }

            Business business = await _businessRepository.FindBusinessById(booking.TimeSlot.BusinessId);

            if (business.OwnerEmail != ownerEmail)
            {
                return HttpCode.Forbidden;
            }

            HttpCode response = await _bookingRepository.DeleteBooking(userEmail, timeSlotId);

            return response;
        }

    }
}