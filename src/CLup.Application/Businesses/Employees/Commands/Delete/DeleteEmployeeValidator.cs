using FluentValidation;

namespace CLup.Application.Businesses.Employees.Commands.Delete
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