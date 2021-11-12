using FluentValidation;

namespace CLup.Domain.Business
{
    public class BusinessOwnerValidator : AbstractValidator<BusinessOwner>
    {
        public BusinessOwnerValidator()
        {
            RuleFor(x => x.UserEmail).NotEmpty();
        }
    }
}