using System;
using System.Collections.Generic;
using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.Messages.ValueObjects;

public sealed class MessageId : ValueObject
{
    public Guid Value { get; private set; }

    private MessageId(Guid value)
    {
        Value = value;
    }

    public static MessageId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}