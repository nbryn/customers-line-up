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
                .ForMember(dest => dest.Business, opts => opts.MapFrom(
                    src => $"{src.TimeSlot.BusinessName} - {src.TimeSlot.Business.Address.Zip}"))
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.TimeSlot.Start.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.Interval, opts => opts.MapFrom(
                    src => src.TimeSlot.Start.TimeOfDay.ToString().Substring(0, 5) + " - " +
                               src.TimeSlot.End.TimeOfDay.ToString().Substring(0, 5)))
                .ForMember(dest => dest.Capacity, opts => opts.MapFrom(
                    src => src.TimeSlot.Bookings.Count().ToString() + "/" + src.TimeSlot.Capacity))
                .ForMember(dest => dest.Latitude, opts => opts.MapFrom(src => src.Business.Coords.Latitude))
                .ForMember(dest => dest.Longitude, opts => opts.MapFrom(src => src.Business.Coords.Longitude))
                .ForMember(dest => dest.Street, opts => opts.MapFrom(src => src.Business.Address.Street))
                .ForMember(dest => dest.UserEmail, opts => opts.MapFrom(src => src.User.Email));
        }
    }
}

