using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Domain.Users.Validation;

public class UserDomainValidator : AbstractValidator<User>
{
    public UserDomainValidator(IValidator<UserData> userDataValidator, IValidator<Address> addressValidator)
    {
        RuleFor(user => user.UserData).NotNull().SetValidator(userDataValidator);
        RuleFor(user => user.Address).NotNull().SetValidator(addressValidator);
    }
}
