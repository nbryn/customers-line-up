using System.Linq;
using AutoMapper;
using CLup.Domain.Booking;
using CLup.Domain.Bookings;

namespace CLup.Application.Queries.Shared
{
    public class BookingDtoMapper : Profile
    {
        public BookingDtoMapper()
        {
            CreateMap<Booking, BookingDto>()
                .ForMember(b => b.Business, s => s.MapFrom(
                    m => $"{m.TimeSlot.BusinessName} - {m.TimeSlot.Business.Address.Zip}"))
                .ForMember(b => b.Date, s => s.MapFrom(m => m.TimeSlot.Start.ToString("dd/MM/yyyy")))
                .ForMember(b => b.Interval, s => s.MapFrom(
                    m => m.TimeSlot.Start.TimeOfDay.ToString().Substring(0, 5) + " - " +
                               m.TimeSlot.End.TimeOfDay.ToString().Substring(0, 5)))
                .ForMember(b => b.Capacity, s => s.MapFrom(
                    m => m.TimeSlot.Bookings.Count().ToString() + "/" + m.TimeSlot.Capacity))
                .ForMember(b => b.Latitude, s => s.MapFrom(m => m.Business.Coords.Latitude))
                .ForMember(b => b.Longitude, s => s.MapFrom(m => m.Business.Coords.Longitude))
                .ForMember(b => b.Street, s => s.MapFrom(m => m.Business.Address.Street))
                .ForMember(b => b.UserEmail, s => s.MapFrom(m => m.User.Email));
        }
    }
}

