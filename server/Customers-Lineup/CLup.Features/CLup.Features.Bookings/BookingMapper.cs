using System.Linq;

using AutoMapper;

using CLup.Domain;
using CLup.Features.Bookings.Commands;

namespace CLup.Features.Bookings
{
    public class BookingMapper : Profile
    {
        public BookingMapper()
        {
            CreateMap<Booking, BookingDTO>()
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
                .ForMember(b => b.Address, s => s.MapFrom(m => m.Business.Address));

            CreateMap<CreateBookingCommand, Booking>();
        }
    }
}

