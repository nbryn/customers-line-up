using System.Threading.Tasks;
using System;

using Data;
using Logic.TimeSlots;
using Logic.Errors;

namespace Logic.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly ITimeSlotRepository _timeSlotRepository;
        private readonly IBookingRepository _bookingRepository;

        public BookingService(ITimeSlotRepository timeSlotRepository, IBookingRepository bookingRepository)
        {
            _timeSlotRepository = timeSlotRepository;
            _bookingRepository = bookingRepository;
        }
        public async Task<int> CreateBooking(string userEmail, int timeSlotId)
        {
            Booking bookingExists = await _bookingRepository.FindBookingById(userEmail, timeSlotId);

            if (bookingExists != null)
            {
                throw new BookingExistsException("Already Booked");
            }

            TimeSlot timeSlot = await _timeSlotRepository.FindTimeSlotById(timeSlotId);

            if (timeSlot.Bookings?.Count >= timeSlot.Capacity)
            {
                throw new Exception("Time Slot is full");
            }

            Booking booking = new Booking { UserEmail = userEmail, TimeSlotId = timeSlotId };

            await _bookingRepository.SaveBooking(booking);

            return 1;
        }
    }
}