using FluentValidation;

namespace CLup.Domain.Businesses.Employees
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.BusinessId).NotEmpty();
        }
    }
}