using System;
using System.Linq;
using AutoMapper;


using CLup.Businesses;
using CLup.TimeSlots.DTO;

namespace CLup.TimeSlots
{
    public class TimeSlotMapper : Profile
    {
        public TimeSlotMapper()
        {
            CreateMap<TimeSlot, TimeSlotDTO>()
                .ForMember(t => t.Business, s => s.MapFrom(m => m.BusinessName))
                .ForMember(t => t.Date, s => s.MapFrom(m => m.Start.ToString("dd/MM/yyyy")))
                .ForMember(t => t.Start, s => s.MapFrom(m => m.Start.TimeOfDay.ToString().Substring(0, 5)))
                .ForMember(t => t.End, s => s.MapFrom(m => m.Start.TimeOfDay.ToString().Substring(0, 5)))
                .ForMember(t => t.Capacity, s => s.MapFrom(m => m.Bookings.Count() + "/" + m.Capacity.ToString()));

            CreateMap<Business, TimeSlot>()
               .ForMember(t => t.BusinessId, s => s.MapFrom(m => m.Id))
               .ForMember(t => t.BusinessName, s => s.MapFrom(m => m.Name))
               .ForMember(t => t.BusinessName, s => s.MapFrom(m => m.Name))
               .ForMember(t => t.Id, s => s.MapFrom(m => Guid.NewGuid().ToString()))
               .ForMember(u => u.CreatedAt, s => s.MapFrom(m => DateTime.Now))
               .ForMember(u => u.UpdatedAt, s => s.MapFrom(m => DateTime.Now));
        }
    }
}