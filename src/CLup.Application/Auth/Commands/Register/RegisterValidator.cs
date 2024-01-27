using FluentValidation;

namespace CLup.Application.Auth.Commands.Register;

public sealed class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(command => command.Email).EmailAddress();
        RuleFor(command => command.Password).NotEmpty();
        RuleFor(command => command.Name).NotEmpty();
        RuleFor(command => command.Zip).NotEmpty();
        RuleFor(command => command.Street).NotEmpty();
        RuleFor(command => command.City).NotEmpty();
        RuleFor(command => command.Latitude).NotEmpty();
        RuleFor(command => command.Longitude).NotEmpty();
    }
}
