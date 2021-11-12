using FluentValidation;

namespace CLup.Application.Queries.Business.Models.Validation
{
    public class BusinessesByOwnerValidator : AbstractValidator<BusinessesByOwnerQuery>
    {
        public BusinessesByOwnerValidator()
        {
            RuleFor(b => b.OwnerEmail).NotNull();
        }
    }
}