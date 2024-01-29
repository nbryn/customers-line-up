using System;
using CLup.Application.Shared;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.TimeSlots.Commands.GenerateTimeSlot;

public sealed class GenerateTimeSlotsCommand : IRequest<Result>
{
    public UserId OwnerId { get; set; }

    public BusinessId BusinessId { get; init; }

    public DateTime Start { get; init; }

    public GenerateTimeSlotsCommand(UserId ownerId, BusinessId businessId, DateTime start)
    {
        OwnerId = ownerId;
        BusinessId = businessId;
        Start = start;
    }
}
