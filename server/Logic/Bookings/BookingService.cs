using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using Logic.Businesses;
using Data;
using Logic.TimeSlots;


using Logic.Context;
using Logic.DTO;

namespace Logic.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly ITimeSlotRepository _timeSlotRepository;
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBusinessRepository businessRepository, ITimeSlotRepository timeSlotRepository,
        IBookingRepository bookingRepository)
        {
            _businessRepository = businessRepository;
            _timeSlotRepository = timeSlotRepository;
            _bookingRepository = bookingRepository;
        }
        public async Task<(Response, string)> VerifyNewBooking(string userEmail, int timeSlotId)
        {
            Booking bookingExists = await _bookingRepository.FindBookingByUser(userEmail, timeSlotId);

            if (bookingExists != null)
            {
                return (Response.Conflict, "You already have a booking for this time slot");
            }

            TimeSlot timeSlot = await _timeSlotRepository.FindTimeSlotById(timeSlotId);

            if (timeSlot == null)
            {
                return (Response.Conflict, "Time slot does not exist");
            }

            if (timeSlot.Bookings?.Count >= timeSlot.Capacity)
            {
                return (Response.Conflict, "This time slot is full");
            }

            Business business = await _businessRepository.FindBusinessById(timeSlot.BusinessId);

            if (business == null)
            {
                //Handle business does not exists - Gather null checks in one place?
            }

            Booking booking = new Booking { UserEmail = userEmail, TimeSlotId = timeSlotId, BusinessId = business.Id };

            await _bookingRepository.SaveBooking(booking);

            return (Response.Created, "Booking successfull");
        }

        public async Task<Response> VerifyDeleteBookingRequest(string ownerEmail, string userEmail, int timeSlotId)
        {
            Booking booking = await _bookingRepository.FindBookingByUser(userEmail, timeSlotId);

            if (booking == null)
            {
                return Response.NotFound;
            }

            Business business = await _businessRepository.FindBusinessById(booking.TimeSlot.BusinessId);

            if (business.OwnerEmail != ownerEmail)
            {
                return Response.Forbidden;
            }

            Response response = await _bookingRepository.DeleteBooking(userEmail, timeSlotId);

            return response;
        }

    }
}