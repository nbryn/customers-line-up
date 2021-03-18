using AutoMapper;
using System.Linq;

using CLup.Bookings.DTO;

namespace CLup.Bookings
{

    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDTO>()
                .ForMember(b => b.Business, s => s.MapFrom(
                    m => $"{ m.TimeSlot.BusinessName} - {m.TimeSlot.Business.Zip.Substring(0, m.TimeSlot.Business.Zip.IndexOf(" "))}"))
                .ForMember(b => b.Date, s => s.MapFrom(m => m.TimeSlot.Start.ToString("dd/MM/yyyy")))
                .ForMember(b => b.Interval, s => s.MapFrom(
                    m => m.TimeSlot.Start.TimeOfDay.ToString().Substring(0, 5) + " - " +
                               m.TimeSlot.End.TimeOfDay.ToString().Substring(0, 5)))
                .ForMember(b => b.Capacity, s => s.MapFrom(
					m => m.TimeSlot.Bookings.Count().ToString() + "/" + m.TimeSlot.Capacity));
        }
    }
}

