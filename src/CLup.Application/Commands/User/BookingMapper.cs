using AutoMapper;
using CLup.Application.Commands.User.CreateBooking;
using CLup.Domain.Booking;
using CLup.Domain.Bookings;

namespace CLup.Application.Commands.User
{
    public class BookingMapper : Profile
    {
        public BookingMapper()
        {
            CreateMap<CreateBookingCommand, Booking>();
        }
    }
}

