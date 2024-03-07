using CLup.Application.Shared;
using CLup.Domain.Bookings;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.TimeSlots.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Application.Bookings.Commands.CreateBooking;

public sealed class CreateBookingCommand : IRequest<Result>
{
    public UserId UserId { get; }

    public BusinessId BusinessId { get; }

    public TimeSlotId TimeSlotId { get; }

    public CreateBookingCommand(UserId userId, BusinessId businessId, TimeSlotId timeSlotId)
    {
        UserId = userId;
        BusinessId = businessId;
        TimeSlotId = timeSlotId;
    }

    public Booking MapToBooking() => new(UserId, BusinessId, TimeSlotId);
}
