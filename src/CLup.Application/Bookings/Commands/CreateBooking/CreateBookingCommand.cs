using System;
using CLup.Application.Shared;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.Bookings.Commands.CreateBooking;

public sealed class CreateBookingCommand : IRequest<Result>
{
    public UserId UserId { get; set; }

    public Guid BusinessId { get; set; }

    public Guid TimeSlotId { get; set; }

    public CreateBookingCommand(UserId userId, Guid timeSlotId, Guid businessId)
    {
        UserId = userId;
        TimeSlotId = timeSlotId;
        BusinessId = businessId;
    }
}
