using FluentValidation;

namespace CLup.Domain.Businesses
{
    public class BusinessOwnerValidator : AbstractValidator<BusinessOwner>
    {
        public BusinessOwnerValidator()
        {
            RuleFor(x => x.UserEmail).NotEmpty();
        }
    }
}