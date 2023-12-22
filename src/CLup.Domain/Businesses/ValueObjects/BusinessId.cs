using System;
using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.Businesses.ValueObjects;

public sealed class BusinessId : Id
{
    private BusinessId(Guid value)
    {
        Value = value;
    }

    public static BusinessId Create(Guid value) => new(value);
}
