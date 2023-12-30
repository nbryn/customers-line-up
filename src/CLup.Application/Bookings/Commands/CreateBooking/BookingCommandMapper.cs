using AutoMapper;
using CLup.Domain.Bookings;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.TimeSlots.ValueObjects;

namespace CLup.Application.Bookings.Commands.CreateBooking;

public sealed class BookingMapper : Profile
{
    public BookingMapper()
    {
        CreateMap<CreateBookingCommand, Booking>()
            .ForMember(dest => dest.BusinessId, opts => opts.MapFrom(src => BusinessId.Create(src.BusinessId)))
            .ForMember(dest => dest.TimeSlotId, opts => opts.MapFrom(src => TimeSlotId.Create(src.TimeSlotId)));
    }
}
