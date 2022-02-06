using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users;
using FluentValidation;

namespace CLup.Domain.User
{
    public class UserValidator : AbstractValidator<Users.User>
    {
        public UserValidator(
            IValidator<UserData> userDataValidator,
            IValidator<Address> addressValidator,
            IValidator<Coords> coordsValidator)
        {
            RuleFor(x => x.UserData).NotNull().SetValidator(userDataValidator);
            RuleFor(x => x.Address).NotNull().SetValidator(addressValidator);
            RuleFor(x => x.Coords).NotNull().SetValidator(coordsValidator);
        }
    }
}