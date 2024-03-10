using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.Employees.ValueObjects;

public sealed class EmployeeId : Id
{
    private EmployeeId(Guid value) : base(value)
    {
    }

    public static EmployeeId Create(Guid value) => new(value);

}
