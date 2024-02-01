using CLup.Application.TimeSlots.Commands.GenerateTimeSlot;
using CLup.Domain.Users.ValueObjects;
using BId = CLup.Domain.Businesses.ValueObjects.BusinessId;

namespace CLup.API.Contracts.TimeSlots.GenerateTimeSlots;

public readonly record struct GenerateTimeSlotsRequest(Guid BusinessId, DateOnly Date)
{
    public GenerateTimeSlotsCommand MapToCommand(UserId userId) => new(userId, BId.Create(BusinessId), Date);
}
