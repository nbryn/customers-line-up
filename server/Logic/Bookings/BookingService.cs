using System.Threading.Tasks;
using System;

using Data;
using Logic.TimeSlots;
using Logic.Errors;
using Microsoft.AspNetCore.Mvc;

using Logic.Context;

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
        public async Task<Response> CreateBooking(string userEmail, int timeSlotId)
        {
            Booking bookingExists = await _bookingRepository.FindBookingById(userEmail, timeSlotId);

            if (bookingExists != null)
            {
                return Response.Conflict;
            }

            TimeSlot timeSlot = await _timeSlotRepository.FindTimeSlotById(timeSlotId);

            if (timeSlot.Bookings?.Count >= timeSlot.Capacity)
            {
                return Response.Conflict;
            }

            Booking booking = new Booking { UserEmail = userEmail, TimeSlotId = timeSlotId };

            await _bookingRepository.SaveBooking(booking);

            return Response.Created;
        }
    }
}