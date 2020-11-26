using System.Threading.Tasks;

using Data;
using Logic.TimeSlots;

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
            TimeSlot timeSlot = await _timeSlotRepository.FindTimeSlotById(timeSlotId);

            if (timeSlot.Bookings?.Count >= timeSlot.Capacity)
            {
                // TODO: Handle
            }

            Booking booking = new Booking { UserEmail = userEmail, TimeSlotId = timeSlotId };

            await _bookingRepository.SaveBooking(booking);

            return 1;
        }
    }
}