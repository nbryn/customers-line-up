using System;
using CLup.Application.Shared;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.TimeSlots.Commands.GenerateTimeSlot;

public sealed class GenerateTimeSlotsCommand : IRequest<Result>
{
    public UserId OwnerId { get; set; }

    public Guid BusinessId { get; set; }

    public DateTime Start { get; set; }
}
