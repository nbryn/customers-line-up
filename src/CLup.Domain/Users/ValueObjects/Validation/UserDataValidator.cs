using FluentValidation;

namespace CLup.Domain.Users.ValueObjects.Validation;

public class UserDataValidator : AbstractValidator<UserData>
{
    public UserDataValidator()
    {
        RuleFor(userData => userData.Name).NotEmpty().MaximumLength(50);
        RuleFor(userData => userData.Email).EmailAddress();
        RuleFor(userData => userData.Password).NotEmpty().MinimumLength(4);
    }
}
