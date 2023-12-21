using FluentValidation;

namespace CLup.Application.Employees.Commands.DeleteEmployee;

public sealed class DeleteEmployeeValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeValidator()
    {
        RuleFor(command => command.BusinessId).NotEmpty();
        RuleFor(command => command.UserId).NotEmpty();
    }
}
