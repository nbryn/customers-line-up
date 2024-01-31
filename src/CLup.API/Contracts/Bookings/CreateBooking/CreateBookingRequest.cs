using CLup.Application.Bookings.Commands.CreateBooking;
using CLup.Domain.Users.ValueObjects;
using BId = CLup.Domain.Businesses.ValueObjects.BusinessId;
using TId = CLup.Domain.TimeSlots.ValueObjects.TimeSlotId;

namespace CLup.API.Contracts.Bookings.CreateBooking;

public readonly record struct CreateBookingRequest(Guid BusinessId, Guid TimeSlotId)
{
    public CreateBookingCommand MapToCommand(UserId userId) =>
        new(userId, BId.Create(BusinessId), TId.Create(TimeSlotId));
}
