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
    TimeOnly Opens,
    TimeOnly Closes,
    int Capacity,
    int TimeSlotLengthInMinutes,
    BusinessType Type)
{
    public UpdateBusinessCommand MapToCommand(UserId userId)
        => new(userId,
            BId.Create(BusinessId),
            new BusinessData(Name, Capacity, TimeSlotLengthInMinutes),
            new Address(Street, Zip, City),
            new Coords(Longitude, Latitude),
            new TimeInterval(Opens, Closes),
            Type);
}
