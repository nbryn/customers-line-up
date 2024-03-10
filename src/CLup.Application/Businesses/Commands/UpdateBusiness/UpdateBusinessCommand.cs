using CLup.Application.Shared;
using CLup.Domain.Businesses.Enums;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Application.Businesses.Commands.UpdateBusiness;

public sealed class UpdateBusinessCommand : IRequest<Result>
{
    public UserId OwnerId { get; }

    public BusinessId BusinessId { get; }

    public BusinessData BusinessData { get; }

    public Address Address { get; }

    public TimeInterval BusinessHours { get; }

    public BusinessType Type { get; }

    public UpdateBusinessCommand(
        UserId ownerId,
        BusinessId businessId,
        BusinessData businessData,
        Address address,
        TimeInterval businessHours,
        BusinessType type)
    {
        OwnerId = ownerId;
        BusinessId = businessId;
        BusinessData = businessData;
        Address = address;
        BusinessHours = businessHours;
        Type = type;
    }
}
