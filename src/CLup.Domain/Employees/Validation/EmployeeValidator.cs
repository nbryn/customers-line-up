using FluentValidation;

namespace CLup.Domain.Employees.Validation;

public class EmployeeValidator : AbstractValidator<Employee>
{
    public EmployeeValidator()
    {
        RuleFor(employee => employee.UserId).NotEmpty();
        RuleFor(employee => employee.BusinessId).NotEmpty();
    }
}
