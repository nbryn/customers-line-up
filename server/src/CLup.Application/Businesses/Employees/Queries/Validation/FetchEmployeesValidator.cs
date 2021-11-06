using FluentValidation;

namespace CLup.Application.Businesses.Employees.Queries.Validation
{
    public class FetchEmployeesValidator : AbstractValidator<FetchEmployeesQuery>
    {
        public FetchEmployeesValidator()
        {
            RuleFor(b => b.BusinessId).NotNull();
        }
    }
}