using FluentValidation;

namespace CLup.API.Contracts.Auth.Validation;

public sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(request => request.Email).EmailAddress();
        RuleFor(request => request.Password).NotEmpty();
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Zip).NotEmpty();
        RuleFor(request => request.Street).NotEmpty();
        RuleFor(request => request.City).NotEmpty();
        RuleFor(request => request.Latitude).NotEmpty();
        RuleFor(request => request.Longitude).NotEmpty();
    }
}
