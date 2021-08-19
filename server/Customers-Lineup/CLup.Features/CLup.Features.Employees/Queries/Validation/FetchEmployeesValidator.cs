using FluentValidation;

namespace CLup.Features.Employees.Queries.Validation
{
    public class FetchEmployeesValidator : AbstractValidator<FetchEmployeesQuery>
    {
        public FetchEmployeesValidator()
        {
            RuleFor(b => b.BusinessId).NotNull();
        }
    }
}