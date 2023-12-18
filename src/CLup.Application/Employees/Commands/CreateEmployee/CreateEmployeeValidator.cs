using FluentValidation;

namespace CLup.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.BusinessId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}