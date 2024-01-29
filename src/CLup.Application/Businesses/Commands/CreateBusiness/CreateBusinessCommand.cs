using CLup.Application.Shared;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Enums;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.Businesses.Commands.CreateBusiness;

public sealed class CreateBusinessCommand : IRequest<Result>
{
    public UserId OwnerId { get; }

    public BusinessData BusinessData { get; }

    public Address Address { get; }

    public Coords Coords { get; }

    public Interval BusinessHours { get; }

    public BusinessType Type { get; }

    public CreateBusinessCommand(UserId ownerId, BusinessData businessData, Address address, Coords coords,
        Interval businessHours, BusinessType type)
    {
        OwnerId = ownerId;
        BusinessData = businessData;
        Address = address;
        Coords = coords;
        BusinessHours = businessHours;
        Type = type;
    }

    public Business MapToBusiness() => new(OwnerId, BusinessData, Address, Coords, BusinessHours, Type);
}
