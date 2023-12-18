using System;
using System.Collections.Generic;
using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.Users.ValueObjects;

public sealed class UserId : ValueObject
{
    public Guid Value { get; private set; }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}