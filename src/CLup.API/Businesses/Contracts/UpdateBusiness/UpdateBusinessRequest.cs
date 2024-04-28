using CLup.Application.Businesses.Commands.UpdateBusiness;
using CLup.Domain.Businesses.Enums;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users.ValueObjects;
using BId = CLup.Domain.Businesses.ValueObjects.BusinessId;

namespace CLup.API.Businesses.Contracts.UpdateBusiness;

public readonly record struct UpdateBusinessRequest(
    Guid BusinessId,
    string Name,
    Address Address,
    TimeInterval BusinessHours,
    int Capacity,
    int TimeSlotLengthInMinutes,
    BusinessType Type)
{
    public UpdateBusinessCommand MapToCommand(UserId userId)
        => new(userId,
            BId.Create(BusinessId),
            new BusinessData(Name, Capacity, TimeSlotLengthInMinutes),
            Address,
            BusinessHours,
            Type);
}
