using System.Linq;
using AutoMapper;
using CLup.Domain.Business.TimeSlot;
using CLup.Domain.Businesses.TimeSlots;

namespace CLup.Application.Shared.Models.Mapping
{
    public class TimeSlotMapper : Profile
    {
        public TimeSlotMapper()
        {
            CreateMap<TimeSlot, TimeSlotDto>()
                .ForMember(t => t.Business, s => s.MapFrom(m => m.BusinessName))
                .ForMember(t => t.Date, s => s.MapFrom(m => m.Start.ToString("dd/MM/yyyy")))
                .ForMember(t => t.Start, s => s.MapFrom(m => m.Start.TimeOfDay.ToString().Substring(0, 5)))
                .ForMember(t => t.Interval, s => s.MapFrom(m => $"{m.Start.TimeOfDay.ToString().Substring(0, 5)} - {m.End.TimeOfDay.ToString().Substring(0, 5)}"))
                .ForMember(t => t.Capacity, s => s.MapFrom(m => $"{m.Bookings.Count()}/{m.Capacity.ToString()}"));
        }
    }
}
    
