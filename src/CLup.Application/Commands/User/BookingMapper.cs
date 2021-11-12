using AutoMapper;
using CLup.Application.Commands.User.Models;
using CLup.Domain.Booking;

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

