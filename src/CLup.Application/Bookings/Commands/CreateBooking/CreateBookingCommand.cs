using System;
using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Bookings.Commands.CreateBooking;

using Shared.Result;

public sealed class CreateBookingCommand : IRequest<Result>
{
    public Guid UserId { get; set; }

    public Guid TimeSlotId { get; set; }

    public Guid BusinessId { get; set; }

    public CreateBookingCommand(Guid userId, Guid timeSlotId, Guid businessId)
    {
        UserId = userId;
        TimeSlotId = timeSlotId;
        BusinessId = businessId;
    }
}
