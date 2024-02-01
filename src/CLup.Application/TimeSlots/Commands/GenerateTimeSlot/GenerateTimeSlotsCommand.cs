using System;
using CLup.Application.Shared;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.TimeSlots.Commands.GenerateTimeSlot;

public sealed class GenerateTimeSlotsCommand : IRequest<Result>
{
    public UserId OwnerId { get; }

    public BusinessId BusinessId { get; }

    public DateOnly Date { get; }

    public GenerateTimeSlotsCommand(UserId ownerId, BusinessId businessId, DateOnly date)
    {
        OwnerId = ownerId;
        BusinessId = businessId;
        Date = date;
    }
}
