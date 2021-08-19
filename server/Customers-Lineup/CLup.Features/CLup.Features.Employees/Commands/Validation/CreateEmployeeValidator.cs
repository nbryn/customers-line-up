using FluentValidation;

namespace CLup.Features.Employees.Commands.Validation
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.BusinessId).NotEmpty();
            RuleFor(x => x.PrivateEmail).EmailAddress();
        }
    }
}