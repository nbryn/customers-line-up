using CLup.Application.Businesses.Commands.UpdateBusiness;
using CLup.Domain.Businesses.Enums;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users.ValueObjects;
using BId = CLup.Domain.Businesses.ValueObjects.BusinessId;

namespace CLup.API.Contracts.Businesses.UpdateBusiness;

public readonly record struct UpdateBusinessRequest(
    Guid BusinessId,
    string Name,
    string Zip,
    string City,
    string Street,
    double Longitude,
    double Latitude,
    int OpensAtHour,
    int OpensAtMinutes,
    int ClosesAtHour,
    int ClosesAtMinutes,
    int Capacity,
    int TimeSlotLength,
    BusinessType Type)
{
    public UpdateBusinessCommand MapToCommand(UserId userId)
        => new(userId,
            BId.Create(BusinessId),
            new BusinessData(Name, Capacity, TimeSlotLength),
            new Address(Street, Zip, City),
            new Coords(Longitude, Latitude),
            new TimeInterval(OpensAtHour, OpensAtMinutes, ClosesAtHour, ClosesAtMinutes),
            Type);
}
