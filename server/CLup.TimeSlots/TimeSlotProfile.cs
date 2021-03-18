using AutoMapper;
using System.Linq;

using CLup.TimeSlots.DTO;

namespace CLup.TimeSlots
{
    public class TimeSlotProfile : Profile
    {
        public TimeSlotProfile()
        {
            CreateMap<TimeSlot, TimeSlotDTO>()
                .ForMember(t => t.Business, s => s.MapFrom(m => m.BusinessName))
                .ForMember(t => t.Date, s => s.MapFrom(m => m.Start.ToString("dd/MM/yyyy")))
                .ForMember(t => t.Start, s => s.MapFrom(m => m.Start.TimeOfDay.ToString().Substring(0, 5)))
                .ForMember(t => t.End, s => s.MapFrom(m => m.Start.TimeOfDay.ToString().Substring(0, 5)))
                .ForMember(t => t.Capacity, s => s.MapFrom(m => m.Bookings.Count() + "/" + m.Capacity.ToString()));
        }
    }
}