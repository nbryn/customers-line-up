using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.Users.ValueObjects;

public sealed class UserId : Id
{
    private UserId(Guid value) : base(value)
    {
    }

    public static UserId Create(Guid value) => new(value);
}
