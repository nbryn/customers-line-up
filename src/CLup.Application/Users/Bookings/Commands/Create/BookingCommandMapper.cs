using AutoMapper;
using CLup.Domain.Bookings;

namespace CLup.Application.Users.Bookings.Commands.Create
{
    public class BookingMapper : Profile
    {
        public BookingMapper()
        {
            CreateMap<CreateBookingCommand, Booking>();
        }
    }
}

