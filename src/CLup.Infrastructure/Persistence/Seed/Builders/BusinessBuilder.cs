using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Enums;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Infrastructure.Persistence.Seed.Builders;

public sealed class BusinessBuilder
{
    private UserId _owner;
    private BusinessData _businessData;
    private Address _address;
    private TimeInterval _businessHours;
    private BusinessType _type;

    public BusinessBuilder WithOwner(UserId ownerId)
    {
        _owner = ownerId;
        return this;
    }

    public BusinessBuilder WithBusinessData(string name, int capacity, int timeSlotLength)
    {
        _businessData = new BusinessData(name, capacity, timeSlotLength);
        return this;
    }

    public BusinessBuilder WithAddress(string street, int zip, string city, Coords coords)
    {
        _address = new Address(street, zip, city, coords);
        return this;
    }

    public BusinessBuilder WithBusinessHours(TimeOnly opens, TimeOnly closes)
    {
        _businessHours = new TimeInterval(opens, closes);
        return this;
    }

    public BusinessBuilder WithType(BusinessType type)
    {
        _type = type;
        return this;
    }

    public Business Build() => new(_owner, _businessData, _address, _businessHours, _type);
}
