using CLup.Application.Businesses.Commands.CreateBusiness;
using CLup.Domain.Businesses.Enums;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.API.Contracts.Businesses.CreateBusiness;

public readonly record struct CreateBusinessRequest(
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
    public CreateBusinessCommand MapToCommand(UserId userId) =>
        new(userId,
            new BusinessData(Name, Capacity, TimeSlotLength),
            new Address(Street, Zip, City),
            new Coords(Longitude, Latitude),
            new TimeInterval(OpensAtHour, OpensAtMinutes, ClosesAtHour, ClosesAtMinutes),
            Type);
}
