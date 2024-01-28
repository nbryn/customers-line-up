using CLup.Application.Bookings.Commands.CreateBooking;
using CLup.Domain.Users.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using BId = CLup.Domain.Businesses.ValueObjects.BusinessId;
using TId = CLup.Domain.TimeSlots.ValueObjects.TimeSlotId;

namespace CLup.API.Contracts.Bookings.CreateBooking;

public record struct CreateBookingRequest
{
    [FromQuery]
    public Guid BusinessId { get; set; }

    [FromRoute]
    public Guid TimeSlotId { get; set; }

    public CreateBookingCommand MapToCommand(UserId userId) =>
        new(userId, BId.Create(BusinessId), TId.Create(TimeSlotId));
}
