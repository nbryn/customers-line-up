using System;
using System.Collections.Generic;
using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.Employees.ValueObjects;

public sealed class EmployeeId : ValueObject
{
    public Guid Value { get; private set; }

    private EmployeeId(Guid value)
    {
        Value = value;
    }

    public static EmployeeId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}