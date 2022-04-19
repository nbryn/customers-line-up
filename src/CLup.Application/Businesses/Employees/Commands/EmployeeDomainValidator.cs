using FluentValidation;
using CLup.Domain.Businesses.Employees;

namespace CLup.Application.Businesses.Employees.Commands
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