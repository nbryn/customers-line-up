using System.Linq;
using AutoMapper;
using CLup.Domain.TimeSlots;

namespace CLup.Application.TimeSlots;

public sealed class TimeSlotMapper : Profile
{
    public TimeSlotMapper()
    {
        CreateMap<TimeSlot, TimeSlotDto>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.BusinessId, opts => opts.MapFrom(src => src.BusinessId.Value))
            .ForMember(dest => dest.Business, opts => opts.MapFrom(src => src.BusinessName))
            .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Start.ToString("dd/MM/yyyy")))
            .ForMember(dest => dest.Start, opts => opts.MapFrom(src => src.Start.TimeOfDay.ToString().Substring(0, 5)))
            .ForMember(dest => dest.Interval, opts => opts.MapFrom(src => $"{src.Start.TimeOfDay.ToString().Substring(0, 5)} - {src.End.TimeOfDay.ToString().Substring(0, 5)}"))
            .ForMember(dest => dest.Capacity, opts => opts.MapFrom(src => $"{src.Bookings.Count()}/{src.Capacity.ToString()}"))
            .ForMember(dest => dest.Available, opts => opts.MapFrom(src => src.IsAvailable()));
    }
}
