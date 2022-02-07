using FluentValidation;

namespace CLup.Application.Businesses.Employees.Commands
{
    public class EmployeeDomainValidator : AbstractValidator<Domain.Businesses.Employees.Employee>
    {
        public EmployeeDomainValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.BusinessId).NotEmpty();
        }
    }
}