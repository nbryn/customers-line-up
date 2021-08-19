using FluentValidation;

namespace CLup.Features.Employees.Commands.Validation
{
    public class DeleteEmployeeValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeValidator()
        {
            RuleFor(x => x.BusinessId).NotEmpty();
            RuleFor(x => x.UserEmail).EmailAddress();
        }
    }
}