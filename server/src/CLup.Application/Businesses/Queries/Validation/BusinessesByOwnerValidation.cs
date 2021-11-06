using FluentValidation;

namespace CLup.Application.Businesses.Queries.Validation
{
    public class BusinessesByOwnerValidator : AbstractValidator<BusinessesByOwnerQuery>
    {
        public BusinessesByOwnerValidator()
        {
            RuleFor(b => b.OwnerEmail).NotNull();
        }
    }
}