using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users;
using CLup.Domain.Users.ValueObjects;
using FluentValidation;

namespace CLup.Application.Users
{
    public class UserDomainValidator : AbstractValidator<User>
    {
        public UserDomainValidator(
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