using CLup.Domain.Shared;

namespace CLup.Domain.Employees;

public static class EmployeeErrors
{
    public static Error NotFound => new("Employees.NotFound", "The employee was not found.");

    public static Error OwnerCannotBeEmployee =>
        new("Employees.OwnerCannotBeEmployee", "A business owner can't be a employee.");
}
