using System.Linq;
using AutoMapper;
using CLup.Domain.Bookings;

namespace CLup.Application.Shared.Bookings
{
    public class BookingDtoMapper : Profile
    {
        public BookingDtoMapper()
        {
            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.Business, s => s.MapFrom(
                    m => $"{m.TimeSlot.BusinessName} - {m.TimeSlot.Business.Address.Zip}"))
                .ForMember(dest => dest.Date, s => s.MapFrom(m => m.TimeSlot.Start.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.Interval, s => s.MapFrom(
                    m => m.TimeSlot.Start.TimeOfDay.ToString().Substring(0, 5) + " - " +
                               m.TimeSlot.End.TimeOfDay.ToString().Substring(0, 5)))
                .ForMember(dest => dest.Capacity, s => s.MapFrom(
                    m => m.TimeSlot.Bookings.Count().ToString() + "/" + m.TimeSlot.Capacity))
                .ForMember(dest => dest.Latitude, s => s.MapFrom(m => m.Business.Coords.Latitude))
                .ForMember(dest => dest.Longitude, s => s.MapFrom(m => m.Business.Coords.Longitude))
                .ForMember(dest => dest.Street, s => s.MapFrom(m => m.Business.Address.Street))
                .ForMember(dest => dest.UserEmail, s => s.MapFrom(m => m.User.Email));
        }
    }
}

