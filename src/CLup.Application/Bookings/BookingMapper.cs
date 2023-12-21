using System.Linq;
using AutoMapper;
using CLup.Domain.Bookings;

namespace CLup.Application.Bookings;

public sealed class BookingMapper : Profile
{
    public BookingMapper()
    {
        CreateMap<Booking, BookingDto>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.UserId.Value))
            .ForMember(dest => dest.BusinessId, opts => opts.MapFrom(src => src.BusinessId.Value))
            .ForMember(dest => dest.TimeSlotId, opts => opts.MapFrom(src => src.TimeSlotId.Value))
            .ForMember(dest => dest.Business, opts => opts.MapFrom(
                src => $"{src.TimeSlot.BusinessName} - {src.Business.Address.Zip}"))
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
