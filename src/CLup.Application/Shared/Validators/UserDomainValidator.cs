using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users;
using FluentValidation;

namespace CLup.Application.Shared.Validators
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