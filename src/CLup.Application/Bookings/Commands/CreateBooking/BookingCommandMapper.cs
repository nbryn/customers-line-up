using AutoMapper;
using CLup.Domain.Bookings;

namespace CLup.Application.Bookings.Commands.CreateBooking
{
    public class BookingMapper : Profile
    {
        public BookingMapper()
        {
            CreateMap<CreateBookingCommand, Booking>();
        }
    }
}

