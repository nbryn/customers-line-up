using FluentValidation;

namespace CLup.Domain.Validation
{
    public class BusinessOwnerValidator : AbstractValidator<BusinessOwner>
    {
        public BusinessOwnerValidator()
        {
            RuleFor(x => x.UserEmail).NotEmpty();
        }
    }
}