using FluentValidation;

namespace CLup.Domain.Business.Employee
{
    public class EmployeeValidator : AbstractValidator<Businesses.Employees.Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.BusinessId).NotEmpty();
        }
    }
}