using System;
using System.Collections.Generic;
using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.Businesses.ValueObjects;

public sealed class BusinessId : ValueObject
{
    public Guid Value { get; private set; }

    private BusinessId(Guid value)
    {
        Value = value;
    }

    public static BusinessId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}