using System.Linq;

using AutoMapper;

using CLup.Domain.Businesses.TimeSlots;

namespace CLup.Features.TimeSlots
{
    public class TimeSlotMapper : Profile
    {
        public TimeSlotMapper()
        {
            CreateMap<TimeSlot, TimeSlotDTO>()
                .ForMember(t => t.Business, s => s.MapFrom(m => m.BusinessName))
                .ForMember(t => t.Date, s => s.MapFrom(m => m.Start.ToString("dd/MM/yyyy")))
                .ForMember(t => t.Start, s => s.MapFrom(m => m.Start.TimeOfDay.ToString().Substring(0, 5)))
                .ForMember(t => t.End, s => s.MapFrom(m => m.End.TimeOfDay.ToString().Substring(0, 5)))
                .ForMember(t => t.Interval, s => s.MapFrom(m => $"{m.Start.TimeOfDay.ToString().Substring(0, 5)} - {m.End.TimeOfDay.ToString().Substring(0, 5)}"))
                .ForMember(t => t.Capacity, s => s.MapFrom(m => $"{m.Bookings.Count()}/{m.Capacity.ToString()}"));
        }
    }
}