using FluentValidation;

namespace CLup.Application.Queries.Business.Owner
{
    public class BusinessesByOwnerValidator : AbstractValidator<BusinessesByOwnerQuery>
    {
        public BusinessesByOwnerValidator()
        {
            RuleFor(b => b.OwnerEmail).NotNull();
        }
    }
}