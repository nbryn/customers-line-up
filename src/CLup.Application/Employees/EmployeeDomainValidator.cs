using CLup.Domain.Employees;
using FluentValidation;

namespace CLup.Application.Employees
{
    public class EmployeeDomainValidator : AbstractValidator<Employee>
    {
        public EmployeeDomainValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.BusinessId).NotEmpty();
        }
    }
}