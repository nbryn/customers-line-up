using FluentValidation;

namespace CLup.Application.Employees.Commands.CreateEmployee;

public sealed class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeValidator()
    {
        RuleFor(command => command.BusinessId).NotEmpty();
        RuleFor(command => command.UserId).NotEmpty();
    }
}
