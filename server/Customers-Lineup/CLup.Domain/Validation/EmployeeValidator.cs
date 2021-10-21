using FluentValidation;

namespace CLup.Domain.Validation
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