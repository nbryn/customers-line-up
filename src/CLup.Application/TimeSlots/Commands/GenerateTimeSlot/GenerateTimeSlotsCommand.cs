using System;
using CLup.Application.Shared;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.TimeSlots.Commands.GenerateTimeSlot;

public sealed class GenerateTimeSlotsCommand : IRequest<Result>
{
    public UserId UserId { get; }

    public BusinessId BusinessId { get; }

    public DateOnly Date { get; }

    public GenerateTimeSlotsCommand(UserId userId, BusinessId businessId, DateOnly date)
    {
        UserId = userId;
        BusinessId = businessId;
        Date = date;
    }
}
