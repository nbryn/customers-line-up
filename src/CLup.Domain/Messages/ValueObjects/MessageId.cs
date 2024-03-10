using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.Messages.ValueObjects;

public sealed class MessageId : Id
{
    private MessageId(Guid value) : base(value)
    {
    }

    public static MessageId Create(Guid value) => new(value);
}
