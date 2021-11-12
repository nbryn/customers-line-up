using FluentValidation;

namespace CLup.Application.Commands.Business.Employee.Create
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