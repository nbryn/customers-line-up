using FluentValidation;

namespace CLup.Application.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeValidator()
        {
            RuleFor(x => x.BusinessId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}