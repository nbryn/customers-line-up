using CLup.Application.Businesses.Commands.CreateBusiness;
using CLup.Domain.Businesses.Enums;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.API.Contracts.Businesses.CreateBusiness;

public readonly record struct CreateBusinessRequest(
    string Name,
    int Zip,
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
    public CreateBusinessCommand MapToCommand(UserId userId) =>
        new(userId,
            new BusinessData(Name, Capacity, TimeSlotLengthInMinutes),
            new Address(Street, Zip, City),
            new Coords(Longitude, Latitude),
            new TimeInterval(Opens, Closes),
            Type);
}
