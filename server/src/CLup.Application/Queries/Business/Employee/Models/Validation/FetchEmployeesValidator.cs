using FluentValidation;

namespace CLup.Application.Queries.Business.Employee.Models.Validation
{
    public class FetchEmployeesValidator : AbstractValidator<FetchEmployeesQuery>
    {
        public FetchEmployeesValidator()
        {
            RuleFor(b => b.BusinessId).NotNull();
        }
    }
}