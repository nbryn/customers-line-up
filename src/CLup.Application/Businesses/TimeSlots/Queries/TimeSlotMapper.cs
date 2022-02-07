using System.Linq;
using AutoMapper;
using CLup.Domain.Businesses.TimeSlots;

namespace CLup.Application.Businesses.TimeSlots.Queries
{
    public class TimeSlotMapper : Profile
    {
        public TimeSlotMapper()
        {
            CreateMap<TimeSlot, TimeSlotDto>()
                .ForMember(dest => dest.Business, src => src.MapFrom(timeSlot => timeSlot.BusinessName))
                .ForMember(dest => dest.Date, src => src.MapFrom(timeSlot => timeSlot.Start.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.Start, src => src.MapFrom(timeSlot => timeSlot.Start.TimeOfDay.ToString().Substring(0, 5)))
                .ForMember(dest => dest.Interval, src => src.MapFrom(timeSlot => $"{timeSlot.Start.TimeOfDay.ToString().Substring(0, 5)} - {timeSlot.End.TimeOfDay.ToString().Substring(0, 5)}"))
                .ForMember(dest => dest.Capacity, src => src.MapFrom(timeSlot => $"{timeSlot.Bookings.Count()}/{timeSlot.Capacity.ToString()}"))
                .ForMember(dest => dest.Available, src => src.MapFrom(timeSlot => timeSlot.IsAvailable()));
        }
    }
}
    
