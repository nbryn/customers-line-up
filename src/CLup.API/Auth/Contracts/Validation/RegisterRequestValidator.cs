using CLup.Domain.Shared.ValueObjects;

namespace CLup.API.Auth.Contracts.Validation;

public sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator(IValidator<Address> addressValidator)
    {
        RuleFor(request => request.Email).EmailAddress();
        RuleFor(request => request.Password).NotEmpty();
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Address).NotEmpty().SetValidator(addressValidator);
    }
}
